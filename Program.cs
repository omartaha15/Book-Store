using Book_Store.Data;
using Book_Store.Models;
using Book_Store.Models.IRepository;
using Book_Store.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );
            
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>( options => options.UseSqlServer( builder.Configuration.GetConnectionString( "Connection" ) ) );
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddAuthentication()
    .AddCookie( options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    } );

builder.Services.AddSession( options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes( 30 );
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
} );

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICartService, CartService>();


var app = builder.Build();


using ( var scope = app.Services.CreateScope() )
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await SeedRolesAsync( roleManager, userManager );
}

async Task SeedRolesAsync ( RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager )
{
    // Define the roles
    var roles = new [] { "Admin", "User" };

    foreach ( var role in roles )
    {
        if ( !(await roleManager.RoleExistsAsync( role )) )
        {
            await roleManager.CreateAsync( new IdentityRole( role ) );
        }
    }

    // Seed an Admin user
    var adminUser = new ApplicationUser
    {
        UserName = "admin@bookstore.com",
        Email = "admin@bookstore.com",
        FirstName = "Admin",
        LastName = "User",
        EmailConfirmed = true,
    };

    string adminPassword = "Admin123!"; // You can customize this

    var user = await userManager.FindByEmailAsync( adminUser.Email );
    if ( user == null )
    {
        var result = await userManager.CreateAsync( adminUser, adminPassword );
        if ( result.Succeeded )
        {
            await userManager.AddToRoleAsync( adminUser, "Admin" );
        }
    }
}


// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Home/Error" );
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );

app.Run();
