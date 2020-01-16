using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeerDB.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string user { get; set; }
        [Required]
        public string beerId { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Comment can only be 1000 characters long")]
        public string comment { get; set; }
        //public byte[] foto { get; set; }
        [Required]
        public DateTime timePosted { get; set; }
    }
}
