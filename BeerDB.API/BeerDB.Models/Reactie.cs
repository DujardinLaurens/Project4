using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeerDB.Models
{
    public class Reactie
    {
        [Key]
        public int Id { get; set; }
        public string Identity_UserID { get; set; }
        public string bierId { get; set; }
        public string commentaar { get; set; }
        //public byte[] foto { get; set; }
        public DateTime timePosted { get; set; }

        public IdentityUser Identity_user { get; set; }
    }
}
