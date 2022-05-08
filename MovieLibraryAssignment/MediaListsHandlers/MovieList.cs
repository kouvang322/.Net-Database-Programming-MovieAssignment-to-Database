using MovieLibraryAssignment.MediaListsHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MovieLibraryAssignment
{
    public class MovieList : IRepository
    {
        List<mymovie> movies = new List<mymovie>();
        public MovieList()
        {
            ReadMovieFromFile();
        }

        public void ReadMovieFromFile()
        {

            string file = "MediaTypes\\Movies-small.csv";
            StreamReader fileReader = new StreamReader(file);
            string line = fileReader.ReadLine();

            if (File.Exists(file))
            {
                // read data from file
                try
                {
                    while (!fileReader.EndOfStream)
                    {
                        mymovie movie = new mymovie();

                        line = fileReader.ReadLine();
                        int idx = line.IndexOf('"');

                        if (idx == -1)
                        {
                            string[] movieInfo = line.Split(",");

                            movie.ID = (int.Parse(movieInfo[0]));

                            movie.Title = (movieInfo[1]);

                            string genresSeparate = movieInfo[2];

                            movie.Genres = genresSeparate.Split("|");

                        }
                        else
                        {
                            movie.ID = (int.Parse(line.Substring(0, idx - 1)));

                            line = line.Substring(idx + 1);

                            idx = line.IndexOf('"');

                            movie.Title = (line.Substring(0, idx + 2));

                            string genreSeparate = line;

                            movie.Genres = genreSeparate.Split("|");
                        }

                        movies.Add(movie);
                    }

                    fileReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }

        public void DisplayMovies()
        {
            foreach (var movie in movies)
            {
                movie.Display();
            }
        }

        public List<Media> Get()
        {
            return new List<Media>(movies);
        }

        public Media Search(string searchString)
        {
            var results = movies.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));

            return results.FirstOrDefault();
        }
    }
}