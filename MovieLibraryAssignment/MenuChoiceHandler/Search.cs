using MovieLibraryAssignment.Context;
using MovieLibraryAssignment.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment.MenuChoiceHandler
{
    class Search
    {
        public Search() { }

        public void SearchMovie()
        {

            Console.WriteLine("What title do you want to search?:");
            var searchString = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var movie = db.Movies.ToList().FirstOrDefault(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

                Console.WriteLine("Movie searched:");
                Console.WriteLine($"\t({movie.Id}) {movie.Title} {movie.ReleaseDate:MM-dd-yyyy}");

                Console.WriteLine("Genres:");
                foreach (var genre in movie.MovieGenres ?? new List<MovieGenre>())
                {
                    Console.WriteLine($"\t{genre.Genre.Name}");
                }
            }
        }

        public void SearchAllMovie()
        {
            {
                var page = 0;
                string userOptionChoice;

                using (var db = new MovieContext())
                {

                    Console.WriteLine("Each page displays 10 movie titles");
                    Console.WriteLine("Which page would you like to see? (1-169):");
                    int userInput = Convert.ToInt32(Console.ReadLine());

                    try
                    {
                        page = userInput;
                        var movieList = db.Movies.ToList();

                        do
                        {
                            foreach (var movie in movieList.Skip((page - 1) * 10).Take(10))
                            {
                                Console.WriteLine($"Movie: ({movie.Id}) {movie.Title}");
                            }

                            Console.WriteLine();
                            Console.WriteLine($"Currently viewing page: {page}");
                            Console.WriteLine("What will you like to do?");
                            Console.WriteLine("1. View next page");
                            Console.WriteLine("2. View previous page");
                            Console.WriteLine("Press any other key to go back.");

                            userOptionChoice = Console.ReadLine();

                            if (userOptionChoice == "1")
                            {
                                if (page == 169)
                                {
                                    Console.WriteLine("You are on the last page.\n");
                                }
                                else
                                {
                                    page += 1;
                                }
                            }

                            if (userOptionChoice == "2")
                            {
                                if (page == 1)
                                {
                                    Console.WriteLine("You are on the first page.\n");
                                }
                                else
                                {
                                    page -= 1;
                                }
                            }

                        } while (userOptionChoice == "1" || userOptionChoice == "2");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
