using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryAssignment.Context;
using MovieLibraryAssignment.DataModels;
using MovieLibraryAssignment.MenuChoiceHandler;
using MovieLibraryAssignment.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
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
                Console.WriteLine("5. Search all Movie records");
                Console.WriteLine("6. Add a new user");
                Console.WriteLine("7. Rate a movie");

                Console.WriteLine("Enter any other key to exit.");
                //  user input
                userChoice = Console.ReadLine();
                _logger.LogInformation("User choice: {choice}", userChoice);

                if (userChoice == "1")
                {
                    try
                    {
                        Search searchList = new Search();
                        searchList.SearchMovie();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _logger.LogInformation("Error occured while searching");
                    }
                }

                if (userChoice == "2")
                {
                    Create createMovie = new Create();
                    createMovie.AddMovie();
                    _logger.LogInformation("Movie was added");
                    _logger.LogInformation("Genres added to new movie");
                }

                if (userChoice == "3")
                {
                    Modify updateRecord = new Modify();
                    updateRecord.UpdateMovie();
                    _logger.LogInformation("Movie was updated");
                }

                if (userChoice == "4")
                {
                    Modify deleteRecord = new Modify();
                    deleteRecord.DeleteMovie();
                    _logger.LogInformation("Movie was deleted");
                }

                if (userChoice == "5")
                {
                    Search searchAll = new Search();
                    searchAll.SearchAllMovie();

                }

                if (userChoice == "6")
                {
                    Create createUser = new Create();
                    createUser.AddUser();
                    _logger.LogInformation("User was added");
                }

                if (userChoice == "7")
                {
                    Modify rate = new Modify();
                    rate.RateMovie();
                    _logger.LogInformation("Rating was added");
                }


            } while
            (userChoice == "1"
            || userChoice == "2"
            || userChoice == "3"
            || userChoice == "4"
            || userChoice == "5"
            || userChoice == "6"
            || userChoice == "7");

            _logger.LogInformation("Program was closed down.");
        }
    }
}


