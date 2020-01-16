using Microsoft.AspNetCore.Identity;

namespace BeerDB.Models
{
    public class BeerDbUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
