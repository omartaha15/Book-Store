using Book_Store.Models;
using Book_Store.Models.IRepository;
using Book_Store.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[Authorize( Roles = "Admin" )]
public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly string _imagePath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", "images" );

    public BookController ( IBookRepository bookRepository, ICategoryRepository categoryRepository )
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index ()
    {
        var books = await _bookRepository.GetAllAsync();
        return View( books );
    }

    public async Task<IActionResult> Details ( int id )
    {
        Book book = await _bookRepository.GetWithDetails( id );
        if ( book == null )
        {
            return NotFound();
        }
        return View( book );
    }

    [HttpGet]
    public async Task<IActionResult> Add ()
    {
        var bookCategoryVM = new BookCategoryVM
        {
            categories = await _categoryRepository.GetAll()
        };
        return View( bookCategoryVM );
    }

    [HttpPost]
    public async Task<IActionResult> Add ( BookCategoryVM bookCategory )
    {
        if ( ModelState.IsValid )
        {
            if ( bookCategory.ImageFile != null && bookCategory.ImageFile.Length > 0 )
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension( bookCategory.ImageFile.FileName )}";
                var filePath = Path.Combine( _imagePath, fileName );

                using ( var stream = new FileStream( filePath, FileMode.Create ) )
                {
                    await bookCategory.ImageFile.CopyToAsync( stream );
                }

                bookCategory.Book.ImageUrl = $"/images/{fileName}";
            }

            await _bookRepository.AddAsync( bookCategory.Book );
            return RedirectToAction( nameof( Index ) );
        }

        bookCategory.categories = await _categoryRepository.GetAll();
        return View( bookCategory );
    }

    public async Task<IActionResult> Edit ( int id )
    {
        var book = await _bookRepository.GetByIdAsync( id );
        if ( book == null )
        {
            return NotFound();
        }

        var bookCategoryVM = new BookCategoryVM
        {
            Book = book,
            categories = await _categoryRepository.GetAll()
        };
        return View( bookCategoryVM );
    }

    [HttpPost]
    public async Task<IActionResult> Edit ( BookCategoryVM bookCategory )
    {
        if ( ModelState.IsValid )
        {
            if ( bookCategory.ImageFile != null && bookCategory.ImageFile.Length > 0 )
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension( bookCategory.ImageFile.FileName )}";
                var filePath = Path.Combine( _imagePath, fileName );

                using ( var stream = new FileStream( filePath, FileMode.Create ) )
                {
                    await bookCategory.ImageFile.CopyToAsync( stream );
                }

                bookCategory.Book.ImageUrl = $"/images/{fileName}";
            }

            await _bookRepository.UpdateAsync( bookCategory.Book );
            return RedirectToAction( nameof( Index ) );
        }

        bookCategory.categories = await _categoryRepository.GetAll();
        return View( bookCategory );
    }

    public async Task<IActionResult> Delete ( int? id )
    {
        if ( id == null )
        {
            return NotFound();
        }

        var book = await _bookRepository.GetByIdAsync( ( int ) id );
        if ( book == null )
        {
            return NotFound();
        }

        return View( book );
    }

    [HttpPost, ActionName( "Delete" )]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed ( int id )
    {
        var book = await _bookRepository.GetByIdAsync( id );
        if ( book != null )
        {
            var imagePath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", book.ImageUrl.TrimStart( '/' ) );
            if ( System.IO.File.Exists( imagePath ) )
            {
                System.IO.File.Delete( imagePath );
            }

            await _bookRepository.DeleteByIdAsync( id );
        }
        return RedirectToAction( nameof( Index ) );
    }
}
