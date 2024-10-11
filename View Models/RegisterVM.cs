using System.ComponentModel.DataAnnotations;

namespace Book_Store.View_Models
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType( DataType.Password )]
        public string Password { get; set; }

        [Required]
        [DataType( DataType.Password )]
        [Compare( "Password" )]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display( Name = "Last Name" )]
        public string LastName { get; set; }
        
    }
}
