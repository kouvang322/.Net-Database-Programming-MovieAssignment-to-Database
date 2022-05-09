using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibraryAssignment.DataModels
{
    public class Occupation
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
