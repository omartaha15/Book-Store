using Book_Store.Models;
using System.Collections.Generic;

namespace Book_Store.ViewModels
{
    public class CartViewModel
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public decimal TotalPrice { get; set; }
    }
}
