using Book_Store.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Book_Store.View_Models
{
    public class BookCategoryVM
    {
        public Book Book { get; set; }
        [ValidateNever]
        public IEnumerable<Category> categories { get; set; }
        public IFormFile ImageFile { get; set; }

        
    }
}
