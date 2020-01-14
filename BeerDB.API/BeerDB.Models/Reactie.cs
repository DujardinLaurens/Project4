using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeerDB.Models
{
    public class Reactie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string gebruiker { get; set; }
        [Required]
        public string bierId { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Commentaar mag mixamaal 1000 karakters bevatten")]
        public string commentaar { get; set; }
        //public byte[] foto { get; set; }
        [Required]
        public DateTime timePosted { get; set; }
    }
}
