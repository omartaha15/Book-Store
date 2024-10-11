namespace Book_Store.Models.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll ();
        Task<Category> GetById ( int id );
        Task DeleteById ( int id );
        Task Add ( Category category );
        Task Update ( Category category );
    }
}
