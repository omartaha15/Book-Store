using Book_Store.Models;
using Book_Store.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync( User );
            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register ( RegisterVM model )
        {
            if ( ModelState.IsValid )
            {
                
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    JoinedDate = DateTime.Now
                };
                var result = await userManager.CreateAsync(user , model.Password);
                if ( result.Succeeded )
                {
                    await userManager.AddToRoleAsync( user, "User" );

                    await signInManager.SignInAsync( user, isPersistent: false );
                    return RedirectToAction("Index", "Home" );
                }
                foreach(var errors in result.Errors )
                {
                    ModelState.AddModelError( "", errors.Description );
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login ( LoginVM model )
        {
            if ( ModelState.IsValid )
            {

                var result = await signInManager.PasswordSignInAsync( model.Email, model.Password, model.RememberMe, lockoutOnFailure: false );
                if ( result.Succeeded )
                {
                    return RedirectToAction( "Index", "Home" );
                }
                else
                {
                    ModelState.AddModelError( string.Empty, "Invalid login attempt." );
                }
            }
            return View( model );
        }

        public async Task<IActionResult> Logout ()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction( "Index", "Home" );
        }
    } 
}
