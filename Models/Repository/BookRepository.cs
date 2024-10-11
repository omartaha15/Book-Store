using Book_Store.Data; // Import your DbContext
using Book_Store.Models;
using Book_Store.Models.IRepository;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context; // Your DbContext

    public BookRepository ( ApplicationDbContext context )
    {
        _context = context; // Initialize the context
    }

    public async Task<IEnumerable<Book>> GetAllAsync ()
    {
        return await _context.Books.ToListAsync(); // Asynchronously get all books
    }

    public async Task<Book> GetByIdAsync ( int id )
    {
        return await _context.Books.FindAsync( id ); // Asynchronously find a book by ID
    }

    public async Task<Book> GetWithDetails ( int id )
    {
        return await _context.Books.Include(b => b.Category).FirstOrDefaultAsync( b => b.Id == id ); 
    }

    public async Task DeleteByIdAsync ( int id )
    {
        var book = await _context.Books.FindAsync( id ); // Find the book to delete
        if ( book != null )
        {
            _context.Books.Remove( book ); // Remove the book
            await _context.SaveChangesAsync(); // Save changes asynchronously
        }
    }

    public async Task AddAsync ( Book book )
    {
        await _context.Books.AddAsync( book ); // Asynchronously add a new book
        await _context.SaveChangesAsync(); // Save changes asynchronously
    }

    public async Task UpdateAsync ( Book book )
    {
        _context.Books.Update( book ); // Update the book
        await _context.SaveChangesAsync(); // Save changes asynchronously
    }
}
