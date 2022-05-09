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
    class Modify
    {
        public Modify()
        {
        }

        public void UpdateMovie()
        {
            Console.WriteLine("Enter movie title to Update: ");
            var oldMovieTitle = Console.ReadLine();

            Console.WriteLine("Enter Updated movie title: ");
            var newMovieTitle = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var updateMovieTitle = db.Movies.FirstOrDefault(x => x.Title == oldMovieTitle);

                // verify if movie exist

                while (updateMovieTitle == null)
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
        }

        public void DeleteMovie()
        {
            Console.WriteLine("Enter movie title to Delete: ");
            var inputMovieTitle = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == inputMovieTitle);
                var deleteGenre = db.MovieGenres.FirstOrDefault(x => x.Movie.Id == deleteMovie.Id);

                // verify exists first
                while (deleteMovie == null)
                {
                    Console.WriteLine("Movie is not in the system, enter another title: ");
                    inputMovieTitle = Console.ReadLine();
                    deleteMovie = db.Movies.FirstOrDefault(x => x.Title == inputMovieTitle);
                }
                Console.WriteLine($"({deleteMovie.Id}) {deleteMovie.Title} {deleteMovie.ReleaseDate}");


                db.Movies.Remove(deleteMovie);
                db.MovieGenres.Remove(deleteGenre);
                Console.WriteLine($"{deleteMovie.Title} was removed");
                db.SaveChanges();
            }
        }

        public void RateMovie()
        {
            Console.WriteLine("Which user is rating? Enter userID: ");
            var userIdPicked = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Which movie would you like to rate?:");
            var moviePickedForRating = Console.ReadLine();

            using (var db = new MovieContext())
            {
                var userChosen = db.Users.ToList().FirstOrDefault(x => x.Id == userIdPicked);
                var movieSelected = db.Movies.ToList().FirstOrDefault(x => x.Title.Contains(moviePickedForRating, StringComparison.CurrentCultureIgnoreCase));

                Console.WriteLine("Movie selected:");
                Console.WriteLine($"\t({movieSelected.Id}) {movieSelected.Title} {movieSelected.ReleaseDate:MM-dd-yyyy}");

                Console.WriteLine("What you would like to rate this movie? (1-5 stars):");

                var userChosenRating = Convert.ToInt32(Console.ReadLine());

                var newRatedMovie = new UserMovie()
                {
                    Rating = userChosenRating,
                    RatedAt = DateTime.Now,
                    User = userChosen,
                    Movie = movieSelected
                };

                db.UserMovies.Add(newRatedMovie);
                db.SaveChanges();

                var userSelected = db.Users.Where(x => x.Id == userIdPicked);
                var users = userSelected.Include(x => x.UserMovies).ThenInclude(x => x.Movie).ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"User: ({user.Id}) {user.Gender} {user.Occupation.Name}");

                    foreach (var movie in user.UserMovies.OrderBy(x => x.Rating))
                    {
                        Console.WriteLine($"\t Rating: {movie.Rating} {movie.RatedAt} Movie Title: {movie.Movie.Title} ");
                    }
                }
            }
        }
    }
}
