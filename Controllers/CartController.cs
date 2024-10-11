using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Book_Store.Models;
using Book_Store.ViewModels;
using Book_Store.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class CartController : Controller
{
    private const string CartSessionKey = "Cart";
    private readonly ApplicationDbContext context;

    public CartController(ApplicationDbContext context )
    {
        this.context = context;
    }

    public async Task<IActionResult> Index ()
    {
        var cartItemIds = GetCart();
        var cartViewModel = new CartViewModel();

        foreach ( var bookId in cartItemIds )
        {
            Book? book =  await  context.Books.FindAsync( bookId )  ;
            if ( book != null )
            {
                cartViewModel.Books.Add( book );
                cartViewModel.TotalPrice += book.Price;
            }
        }

        return View( cartViewModel );
    }


    public IActionResult AddToCart ( int bookId )
    {
        var cart = GetCart();
        cart.Add( bookId );
        HttpContext.Session.SetObjectAsJson( CartSessionKey, cart );
        return RedirectToAction( "Index", "Home" );
    }

    public IActionResult RemoveFromCart ( int bookId )
    {
        var cart = GetCart();
        cart.Remove( bookId );
        HttpContext.Session.SetObjectAsJson( CartSessionKey, cart );
        return RedirectToAction( "Index" );
    }

    private List<int> GetCart ()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<int>>( CartSessionKey ) ?? new List<int>();
        return cart;
    }
}
