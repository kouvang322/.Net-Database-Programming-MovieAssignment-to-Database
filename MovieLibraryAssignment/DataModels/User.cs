using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibraryAssignment.DataModels
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public long Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<UserMovie> UserMovies {get;set;}
    }
}
