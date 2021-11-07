using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models
{
    // Model for a dvd
    public class Dvd
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public string Notes { get; set; }
    }
}