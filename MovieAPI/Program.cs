global using MovieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.connection;

namespace MovieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.MapGet("API/users/all", async () =>
            {
                //Hämta alla personer i systemet Confirmed
                RepositoryContext context = new RepositoryContext();


                UserRepository userRepo = new UserRepository(context);

                return userRepo.GetAll();
            });

            app.MapGet("API/movies/search/", async (string movie) =>
            {
                // Search movie, repository pattern

                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetByTitle(movie);


            });

            app.MapGet("API/movies/bydbid", async (int dbID) =>
            {
                // Search movie, repository pattern

                //Movie newMovie = new Movie();

                RepositoryContext context = new RepositoryContext();
                MovieRepository movieRepo = new MovieRepository(context);
                UserMovieRepository UserMovieRepo = new UserMovieRepository(context);
                TMDBRepository TMDBRepo = new TMDBRepository();


                var movieInDb = movieRepo.GetByCondition(m => m.Id == dbID);

                var movie = await TMDBRepo.GetMovieByID(movieInDb.OrderBy(m => m.Id).Last().ExtID);


                //newMovie.ExtID = movie.ExtID;
                //newMovie.Title = movie.Title;
                //newMovie.Link = $"https://www.themoviedb.org/movie/{newMovie.ExtID}-{newMovie.Title}";
                //newMovie.Description = movie.Overview;


                return movie;

            });

            app.MapPost("API/movie/userlink", async (UserMovie newUserMovie, int userID, int extId, int? rating) =>
            {
                // Link a user to a movie.
                // IF movie does not exist in DB, it will be added.


                RepositoryContext context = new RepositoryContext();
                MovieRepository movieRepo = new MovieRepository(context);
                UserMovieRepository UserMovieRepo = new UserMovieRepository(context);



                //If movie does not exist in db
                var movieInDb = movieRepo.GetByCondition(m => m.ExtID == extId);

                if (movieInDb.IsNullOrEmpty())
                {
                    //Create movie if not existing
                    await CreateMovie(extId);

                }
                movieInDb = movieRepo.GetByCondition(m => m.ExtID == extId);

                newUserMovie.UserID = userID;
                newUserMovie.MovieID = movieInDb.OrderBy(m => m.Id).Select(m => m.Id).LastOrDefault();
                if (rating.HasValue)
                {
                    if (rating > 5 || rating < 1)
                    {
                        return Results.BadRequest("Inavlid rating, should be bewteen 1 and 5");
                    }
                    newUserMovie.UserRating = (int)rating;
                }

                UserMovieRepo.Create(newUserMovie);
                context.SaveChanges();

                return Results.Ok(newUserMovie);


            });

            app.MapGet("API/movies/{userId}", (int userId) =>
            {
                //Get genres from DB by id, repository pattern

                RepositoryContext context = new RepositoryContext();

                GenreRepository genreRepo = new GenreRepository(context);
                UserMovieRepository userMovieRepo = new UserMovieRepository(context);

                var response = userMovieRepo.GetByCondition(umr => umr.UserID == userId).Select(umr => new MovieNameRating { MovieName = umr.MovieName, UserRating = umr.UserRating });

                return Results.Ok(response);
            });


            app.MapGet("API/movies/suggestion/{userId}", async (int userId) =>
            {
                //Return movie suggestion based on users genres

                RepositoryContext context = new RepositoryContext();


                GenreRepository genreRepo = new GenreRepository(context);
                UserMovieRepository userMovieRepo = new UserMovieRepository(context);


                var movieIds = userMovieRepo.GetByCondition(umr => umr.UserID == userId)
                                .Select(umr => umr.MovieID)
                                .ToList();

                var genres = genreRepo.GetByCondition(g => movieIds.Contains(g.Id))
                          .Select(g => g.ExtID)
                          .Distinct()
                          .ToList();

                string requestpath = "";

                foreach (var item in genres)
                {
                    requestpath += item.ToString() + "%7C";
                }
            
                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetByGenres(requestpath);

            });

            app.MapGet("API/genres/{userId}", (int userId) =>
            {
                //Get genres from DB by id, repository pattern

                RepositoryContext context = new RepositoryContext();


                GenreRepository genreRepo = new GenreRepository(context);
                UserMovieRepository userMovieRepo = new UserMovieRepository(context);

                var movieIds = userMovieRepo.GetByCondition(umr => umr.UserID == userId)
                                .Select(umr => umr.MovieID)
                                .ToList();

                var genres = genreRepo.GetByCondition(g => movieIds.Contains(g.Id))
                          .Select(g => g.Title)
                          .Distinct()
                          .ToList();


                return genres;
            });

            static async Task CreateMovie(int id)
            {
                //Method to add movie to database, if needed.
                Movie newMovie = new Movie();

                RepositoryContext context = new RepositoryContext();


                MovieRepository movieRepo = new MovieRepository(context);

                TMDBRepository TMDBRepo = new TMDBRepository();

                var movie = await TMDBRepo.GetByID(id);

                newMovie.ExtID = movie.ExtID;
                newMovie.Title = movie.Title;
                newMovie.Link = $"https://www.themoviedb.org/movie/{newMovie.ExtID}-{newMovie.Title}";
                newMovie.Description = movie.Overview;


                movieRepo.Create(newMovie);
                context.SaveChanges();

      
                MovieGenreRepository MovieGenreRepo = new MovieGenreRepository(context);

                GenreRepository genreRepo = new GenreRepository(context);

                foreach (var item in movie.Gendres)
                {
                    MovieGenre newMovieGenre = new MovieGenre();

                    var GenID = genreRepo.GetByCondition(g => g.ExtID == item.ExtID);

                    newMovieGenre.MovieID = newMovie.Id;
                    newMovieGenre.GenreID = GenID.OrderBy(g => g.Id).Select(g => g.Id).LastOrDefault();

                    MovieGenreRepo.Create(newMovieGenre);
                    context.SaveChanges();
                }

            }

            app.Run();

        }
    }
}