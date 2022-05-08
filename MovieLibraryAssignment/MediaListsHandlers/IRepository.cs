using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment.MediaListsHandlers
{
    interface IRepository
    {
        List<Media> Get();
        Media Search(string searchString);
    }
}
