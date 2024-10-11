namespace Book_Store.Models.IRepository
{
    public interface ICartService
    {
        void AddToCart ( CartItem item );
        void RemoveFromCart ( int itemId );
        void ClearCart ();
        List<CartItem> GetCartItems ();
    }
}
