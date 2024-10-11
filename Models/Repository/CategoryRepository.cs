using Book_Store.Data;
using Book_Store.Models.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository ( ApplicationDbContext context )
        {
            this.context = context;
        }

        public async Task Add ( Category category )
        {
            await context.AddAsync( category );
            await context.SaveChangesAsync();
        }

        public async Task DeleteById ( int id )
        {
            var category = await GetById( id );
            context.Remove( category );
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetById ( int id )
        {
            return await context.Categories.FindAsync( id );
        }

        public async Task Update ( Category category )
        {
            context.Categories.Update( category );
            await context.SaveChangesAsync();
        }
    }
}
