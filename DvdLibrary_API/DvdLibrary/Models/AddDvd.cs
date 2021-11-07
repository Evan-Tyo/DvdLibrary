using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models
{
    // Model for adding a dvd
    public class AddDvd
    {
        [Required(ErrorMessage = "Please enter the title information.")]
        public string Title { get; set; }

        [Range(1000, 9999, ErrorMessage = "Please enter a 4-digit year.")]
        public int ReleaseYear { get; set; }

        public string Director { get; set; }

        public string Rating { get; set; }

        public string Notes { get; set; }
    }
}