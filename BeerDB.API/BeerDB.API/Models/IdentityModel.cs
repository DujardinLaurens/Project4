using System.ComponentModel.DataAnnotations;

namespace BeerDB.API.Models
{
    public class IdentityModel
    {
        //Alle Identity elementen die via Javascript/JSON verstuurd worden
        //Basis voor CookieAutenticate en/of JWT authenticatie
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}