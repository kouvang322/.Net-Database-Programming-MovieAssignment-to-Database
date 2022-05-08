using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    public abstract class Media
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public abstract void Display();
    }
}
