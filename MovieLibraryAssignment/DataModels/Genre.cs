using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MovieLibraryAssignment.DataModels
{
    public class Genre
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
    }
}
