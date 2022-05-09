using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibraryAssignment.DataModels
{
    public class Movie
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
        public virtual ICollection<UserMovie> UserMovies {get;set;}
    }
}
