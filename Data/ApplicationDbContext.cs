using Book_Store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {


            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<Book>()
            .HasOne( b => b.Category )
            .WithMany( c => c.Books )
            .HasForeignKey( b => b.CategoryId )
            .OnDelete( DeleteBehavior.Cascade);

            // Seeding Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", Description = "Fictional books", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Science", Description = "Books on Science", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Biography", Description = "Biographies of famous people", DisplayOrder = 3 }
            );

            // Seeding Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m, Description = "A novel about the American dream.", CategoryId = 1 },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", Price = 8.99m, Description = "Dystopian novel set in a totalitarian society.", CategoryId = 1 },
                new Book { Id = 3, Title = "A Brief History of Time", Author = "Stephen Hawking", Price = 15.99m, Description = "A popular science book on cosmology.", CategoryId = 2 },
                new Book { Id = 4, Title = "The Diary of a Young Girl", Author = "Anne Frank", Price = 7.99m, Description = "A diary of a Jewish girl during WWII.", CategoryId = 3 }
            );
        }
    }
}
