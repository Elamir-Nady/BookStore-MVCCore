using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BookAutherViewModel
    {
        public int BookID { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MaxLength(120)]
        [MinLength(5)]
        public string Descryption { get; set; }
        public IFormFile File { get; set; }
        public string ImageURL { get; set; }
        public int AutherID { get; set; }
        public List<Auther> Authers { get; set; }

    }
}
