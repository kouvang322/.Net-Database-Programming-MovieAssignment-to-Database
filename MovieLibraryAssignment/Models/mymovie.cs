using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    public class mymovie : Media
    {

        public string[] Genres { get; set; }

        public override void Display()
        {

           Console.WriteLine($"ID: {ID}, Title: {Title}, Genre(s): {string.Join(", ", Genres)}");
           
        }

        public override string ToString()
        {
            // Hardcoded the type of Media to the corresponding title
            // Not the way best way to figure out the type
            return $"ID: {ID}, Title: {Title}, Type: Movie";
        }
    }
}
