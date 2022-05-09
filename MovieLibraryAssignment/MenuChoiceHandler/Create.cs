using Microsoft.EntityFrameworkCore;
using MovieLibraryAssignment.Context;
using MovieLibraryAssignment.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryAssignment.MenuChoiceHandler
{
    public class Create
    {
        public Create()
        {

        }

        public void AddMovie()
        {
            Console.WriteLine("Enter NEW Movie Title: ");
            var newtitle = Console.ReadLine();

            Console.WriteLine("Enter new movie release date: ");
            var inputDate = Console.ReadLine();

            DateTime validDate;
            var isDateValid = DateTime.TryParse(inputDate, out validDate);

            while (!isDateValid)
            {
                Console.WriteLine("Date entered was not a valid date. Please enter correct date.");
                inputDate = Console.ReadLine();
                isDateValid = DateTime.TryParse(inputDate, out validDate);
            }

            DateTime releaseDate = Convert.ToDateTime(inputDate);

            var yesOrNoChoice = "";

            using (var db = new MovieContext())
            {
                var movie = new Movie()
                {
                    Title = newtitle,
                    ReleaseDate = releaseDate
                };
                db.Movies.Add(movie);
                db.SaveChanges();

                do
                {
                    Console.WriteLine("Enter a genre for the new movie: ");
                    var addGenreToMovie = Console.ReadLine();
                    var inputGenres = db.Genres.ToList().FirstOrDefault(x => x.Name.Contains(addGenreToMovie, StringComparison.CurrentCultureIgnoreCase));

                    if (inputGenres == null)
                    {
                        var createdNewGenre = new Genre()
                        {
                            Name = addGenreToMovie
                        };
                        db.Genres.Add(createdNewGenre);
                        db.SaveChanges();

                        var newMovieGenres = new MovieGenre()
                        {
                            Movie = movie,
                            Genre = createdNewGenre
                        };
                        db.MovieGenres.Add(newMovieGenres);
                        db.SaveChanges();
                    }
                    else
                    {
                        var newMovieGenres = new MovieGenre()
                        {
                            Movie = movie,
                            Genre = inputGenres
                        };
                        db.MovieGenres.Add(newMovieGenres);
                        db.SaveChanges();
                    }

                    Console.WriteLine("Would you like to add another genre? (Y/N):");
                    yesOrNoChoice = Console.ReadLine().ToUpper();
                    if (yesOrNoChoice != "Y" && yesOrNoChoice != "N")
                    {
                        Console.WriteLine("Please enter 'Y' to add another genre or 'N' to stop adding.");
                        yesOrNoChoice = Console.ReadLine().ToUpper();
                    }

                } while (yesOrNoChoice != "N");


                var newMovie = db.Movies.FirstOrDefault(x => x.Title == newtitle);

                Console.WriteLine($"\t Movie:");
                Console.WriteLine($"({newMovie.Id}) {newMovie.Title} {newMovie.ReleaseDate}");
            }

        }

        public void AddUser()
        {
            Console.WriteLine("Enter new user age:");
            int newUserAge = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter new user gender (M/F):");
            string newUserGender = Console.ReadLine().ToUpper();

            while (newUserGender != "M" && newUserGender != "F")
            {
                Console.WriteLine("Enter either 'M' or 'F' for the gender.");
                newUserGender = Console.ReadLine().ToUpper();
            }

            Console.WriteLine("Enter new user zip code:");
            var newUserZip = (Console.ReadLine());

            using (var db = new MovieContext())
            {
                var userOccupations = db.Occupations.ToList();

                Console.WriteLine("Occupations:");
                foreach (var occupation in userOccupations)
                {
                    Console.WriteLine($"\t{occupation.Id} {occupation.Name}");
                }

                Console.WriteLine("What is the occupation of the new user?:");
                var newUserOccupation = Console.ReadLine();

                var enteredOccupation = db.Occupations.ToList().FirstOrDefault(x => x.Name.Contains(newUserOccupation, StringComparison.CurrentCultureIgnoreCase));

                if (enteredOccupation == null)
                {
                    var newOccupation = new Occupation()
                    {
                        Name = newUserOccupation
                    };

                    db.Occupations.Add(newOccupation);
                    db.SaveChanges();

                    var user = new User()
                    {
                        Age = newUserAge,
                        Gender = newUserGender,
                        ZipCode = newUserZip,
                        Occupation = newOccupation
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    var user = new User()
                    {
                        Age = newUserAge,
                        Gender = newUserGender,
                        ZipCode = newUserZip,
                        Occupation = enteredOccupation
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                var newUser = db.Users.Include(x => x.Occupation).ToList().LastOrDefault(x => x.Age == newUserAge);
                Console.WriteLine($" Added: ({newUser.Id}) {newUser.Age} {newUser.Gender} {newUser.ZipCode} {newUser.Occupation.Name}");

            }
        }
    }
}
