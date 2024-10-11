using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength( 100 )]
        public string Author { get; set; }
        [Required]
        [Range( 1, 5000)]
        public Decimal Price { get; set; }
        [Required]
        [StringLength( 2000)]
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        
    }
}
