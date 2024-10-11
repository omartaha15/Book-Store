using Book_Store.Models;
using Book_Store.Models.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Authorize( Roles = "Admin" )]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController ( ICategoryRepository categoryRepository )
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index ()
        {
            var categories = await _categoryRepository.GetAll();
            return View( categories );
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create ( Category category )
        {
            if ( ModelState.IsValid )
            {
                await _categoryRepository.Add( category );
                return RedirectToAction( "Index" );
            }
            return View( category );
        }

        [HttpGet]
        public async Task<IActionResult> Edit ( int id )
        {
            var category = await _categoryRepository.GetById( id );
            if ( category == null )
            {
                return NotFound();
            }
            return View( category );
        }

        [HttpPost]
        public async Task<IActionResult> Edit ( Category category )
        {
            if ( ModelState.IsValid )
            {
                await _categoryRepository.Update( category );
                return RedirectToAction( nameof( Index ) );
            }
            return View( category );
        }

        [HttpGet]
        public async Task<IActionResult> Delete ( int id )
        {
            var category = await _categoryRepository.GetById( id );
            if ( category == null )
            {
                return NotFound();
            }
            return View( category );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed ( int id )
        {
            var category = await _categoryRepository.GetById( id );
            if ( category != null )
            {
                await _categoryRepository.DeleteById( id );
            }
            return RedirectToAction( nameof( Index ) );
        }
    }
}
