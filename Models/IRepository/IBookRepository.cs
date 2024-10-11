namespace Book_Store.Models.IRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync (); 
        Task<Book> GetByIdAsync ( int id ); 
        Task DeleteByIdAsync ( int id ); 
        Task<Book> GetWithDetails ( int id );
        Task AddAsync ( Book book ); 
        Task UpdateAsync ( Book book ); 
    }
}
