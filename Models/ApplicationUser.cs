using Microsoft.AspNetCore.Identity;

namespace Book_Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public ApplicationUser ()
        {
            JoinedDate = DateTime.Now;
        }
    }
}
