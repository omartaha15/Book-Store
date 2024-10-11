using Book_Store.Models.IRepository;
using Microsoft.AspNetCore.Http;

namespace Book_Store.Models.Repository
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cartItems = new List<CartItem>();

        public void AddToCart ( CartItem item )
        {
            var existingItem = _cartItems.FirstOrDefault( i => i.Id == item.Id );
            if ( existingItem != null )
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cartItems.Add( item );
            }
        }

        public void RemoveFromCart ( int itemId )
        {
            var item = _cartItems.FirstOrDefault( i => i.Id == itemId );
            if ( item != null )
            {
                _cartItems.Remove( item );
            }
        }

        public void ClearCart ()
        {
            _cartItems.Clear();
        }

        public List<CartItem> GetCartItems ()   
        {
            return _cartItems;
        }
    }
}
