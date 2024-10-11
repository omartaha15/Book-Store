using Book_Store.Data;
using Book_Store.Models;
using Book_Store.Models.IRepository;
using Book_Store.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController ( ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index ()
        {
            var Books = await context.Books.Include( b => b.Category ).ToListAsync();

            return View( Books );
        }

        public async Task<IActionResult> Details ( int id )
        {
            Book book = await context.Books.Include(b => b.Category).FirstOrDefaultAsync( b => b.Id == id );
            if ( book == null )
            {
                return NotFound();
            }
            return View( book );
        }

        public IActionResult Contact ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContactUs ( ContactUsViewModel model )
        {
            if ( ModelState.IsValid )
            {
                TempData [ "SuccessMessage" ] = "Your message has been sent successfully!";
                return RedirectToAction( "Contact" );
            }

            return View( "Contact", model );
        }


        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error ()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
