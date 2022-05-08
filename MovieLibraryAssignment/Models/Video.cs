using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    class Video : Media
    {
        public string Format { get; set; }
        public int Length { get; set; }
        public int[] Regions { get; set; }

        public override void Display()
        {
            Console.WriteLine($"ID: {ID}, Title: {Title}, Format: {Format}, Length: {Length}, Region(s): {string.Join(", ", Regions)}");
        }

        public override string ToString()
        {
            // Hardcoded the type of Media to the corresponding title
            // Not the way best way to figure out the type
            return $"ID: {ID}, Title: {Title}, Type: Video";
        }
    }
}
