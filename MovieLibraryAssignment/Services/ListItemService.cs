using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryAssignment.Context;
using MovieLibraryAssignment.DataModels;
using MovieLibraryAssignment.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment
{
    public class ListItemService : IListItemService
    {

        private readonly ILogger<IListItemService> _logger;

        public ListItemService(ILogger<IListItemService> logger)
        {
            _logger = logger;
            //Read();
        }

        // This method displays the options for the user
        // Options lead to other methods 
        public void DisplayMenu()
        {
            string userChoice;

            do
            {
                // Ask user what they want to do
                Console.WriteLine();
                Console.WriteLine("1. Search Movie Title");
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Update Movie");
                Console.WriteLine("4. Delete Movie");
                Console.WriteLine("Enter any other key to exit.");
                //  user input
                userChoice = Console.ReadLine();
                _logger.LogInformation("User choice: {choice}", userChoice);

                if (userChoice == "1")
                {
                    try
                    {
                        Console.WriteLine("What title do you want to search?:");
                        var searchString = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                           var movie = db.Movies.ToList().FirstOrDefault(x => x.Title.Contains(searchString,StringComparison.CurrentCultureIgnoreCase));
                           Console.WriteLine($"({movie.Id}) {movie.Title}");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
                
                if (userChoice == "2")
                {
                    Console.WriteLine("Enter NEW Movie Title: ");
                    var title = Console.ReadLine();

                    Console.WriteLine("Enter new movie release date: ");
                    var inputDate = Console.ReadLine();
                    DateTime releaseDate = Convert.ToDateTime(inputDate);
                    
                    using (var db = new MovieContext())
                    {
                        var movie = new Movie()
                        {
                            Title = title,
                            ReleaseDate = releaseDate
                        };
                        db.Movies.Add(movie);
                        db.SaveChanges();

                        var newMovie = db.Movies.FirstOrDefault(x => x.Title == title);
                        Console.WriteLine($"({newMovie.Id}) {newMovie.Title} {newMovie.ReleaseDate}");

                    }
                    _logger.LogInformation("Movie was added");
                }

                if (userChoice == "3")
                {
                    Console.WriteLine("Enter movie title to Update: ");
                    var oldMovieTitle = Console.ReadLine();

                    Console.WriteLine("Enter Updated movie title: ");
                    var newMovieTitle = Console.ReadLine();

                    using (var db = new MovieContext())
                    {
                        var updateMovieTitle = db.Movies.FirstOrDefault(x => x.Title == oldMovieTitle);
                
                        // verify if movie exist
                        
                        while(updateMovieTitle == null)
                        {
                            Console.WriteLine("Movie is not in the system, enter another title: ");
                            oldMovieTitle = Console.ReadLine();
                            updateMovieTitle = db.Movies.FirstOrDefault(x => x.Title == oldMovieTitle);
                        }

                        Console.WriteLine($"({updateMovieTitle.Id}) {updateMovieTitle.Title}");

                        updateMovieTitle.Title = newMovieTitle;

                        db.Movies.Update(updateMovieTitle);
                        db.SaveChanges();

                        var updatedMovieTitle = db.Movies.FirstOrDefault(x => x.Title == newMovieTitle);
                        Console.WriteLine($"({updatedMovieTitle.Id}) {updatedMovieTitle.Title} {updatedMovieTitle.ReleaseDate}");
                    }
                    _logger.LogInformation("Movie was updated");
                }

                else if (userChoice == "4")
                {
                    Console.WriteLine("Enter movie tile to Delete: ");
                    var inputMovieTitle = Console.ReadLine();

                    using (var db = new MovieContext())
                    {
                        var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == inputMovieTitle);

                        // verify exists first
                        while(deleteMovie == null)
                        {
                            Console.WriteLine("Movie is not in the system, enter another title: ");
                            inputMovieTitle = Console.ReadLine();
                            deleteMovie = db.Movies.FirstOrDefault(x => x.Title == inputMovieTitle);
                        }
                        Console.WriteLine($"({deleteMovie.Id}) {deleteMovie?.Title} {deleteMovie.ReleaseDate}");

                        db.Movies.Remove(deleteMovie);
                        Console.WriteLine($"{deleteMovie.Title} was removed");
                        db.SaveChanges();
                    }
                    _logger.LogInformation("Movie was deleted");
                }

            } while (userChoice == "1" || userChoice == "2" || userChoice == "3" || userChoice == "4");

            _logger.LogInformation("Program was closed down.");
        }
    }
}

