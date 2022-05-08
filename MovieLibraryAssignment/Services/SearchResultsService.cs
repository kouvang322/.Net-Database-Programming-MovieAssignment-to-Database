using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment.Services
{
    class SearchResultsService : ISearchResultsService
    {

        private readonly List<Media> _mediaList = new();
        private readonly MovieList _movieList;
        private readonly ShowList _showList;
        private readonly VideoList _videoList;

        public SearchResultsService()
        {
            _movieList = new MovieList();
            _showList = new ShowList();
            _videoList = new VideoList();
        }

        public List<Media> SearchAllMedia(string searchString)
        {

            _mediaList.Add(_movieList.Search(searchString));
            _mediaList.Add(_showList.Search(searchString));
            _mediaList.Add(_videoList.Search(searchString));

            return _mediaList;
        }

    }
}
