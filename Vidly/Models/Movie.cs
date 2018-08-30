using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name="Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        
        public GenreType GenreType { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreTypeId { get; set; }

        [Display(Name ="Number in Stock")]
        public byte InStock { get; set; }

    }
}