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

                var getMovie = movieInDb.OrderBy(m => m.ExtID).Last();
                var movie = await TMDBRepo.GetMovieByID(getMovie.ExtID);


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
                    await CreateMovie(extId, userID);

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

                var response = userMovieRepo.GetByCondition(umr => umr.UserID == userId).Select(umr => new MovieNameRating { MovieName = umr.MovieName, UserRating = umr.UserRating, MovieID = umr.MovieID });

                return Results.Ok(response);
            });


            app.MapGet("API/movies/suggestion/{userId}", async (int userId) =>
            {
                //Return movie suggestion based on users genres

                RepositoryContext context = new RepositoryContext();

                GenreRepository genreRepo = new GenreRepository(context);
                UserMovieRepository userMovieRepo = new UserMovieRepository(context);
                MovieGenreRepository movieGenreRepo = new MovieGenreRepository(context);

                
                var movieIds = GetMovieDbIDs(userId, userMovieRepo);

                // For each movieID, get respective genre(s)
                List<MovieGenre> genres = new List<MovieGenre> { };
                
                foreach (var item in movieIds)
                {
                    genres.AddRange(movieGenreRepo.GetByCondition(m => m.MovieID == item));

                };


                // For each genre, add genre ID to serch string
                string requestpath = "";

                foreach (var item in genres)
                {
                    var genreID = genreRepo.GeExtIdByID(item.GenreID);


                    requestpath += genreID + "%7C";
                }
            
                // Get result from TMDb based on search term
                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetByGenres(requestpath);

            });


            app.MapGet("API/genres/{userId}", (int userId) =>
            {
                

                RepositoryContext context = new RepositoryContext();


                GenreRepository genreRepo = new GenreRepository(context);
                UserMovieRepository userMovieRepo = new UserMovieRepository(context);
                MovieGenreRepository movieGenreRepo = new MovieGenreRepository(context);


                var movieIds = GetMovieDbIDs(userId, userMovieRepo);

                // For each movieID, get respective genre(s)
                List<MovieGenre> genres = new List<MovieGenre> { }; 

                //Get movies genreIDs from movegenre by moveID
                foreach (var item in movieIds)
                {
                    genres.AddRange(movieGenreRepo.GetByCondition(m => m.MovieID == item));
                          
                };

                // For each genre, get genre details like Title, description etc.
                List<Genre> genreDetails = new List<Genre> { };

                foreach (var item in genres)
                {
                    genreDetails.AddRange(genreRepo.GetByCondition(g => g.Id == item.GenreID));
                }


                return genreDetails;
            });

            static List<int> GetMovieDbIDs(int userID, UserMovieRepository repo)
            {
                // Return a list of movie IDs connected to a user
                var result = repo.GetByCondition(umr => umr.UserID == userID)
                                .Select(umr => umr.MovieID)
                                .ToList();

                return result;
            }

            static async Task CreateMovie(int extID, int userID)
            {
                //Method to add movie to database, if needed.
                Movie newMovie = new Movie();

                RepositoryContext context = new RepositoryContext();


                MovieRepository movieRepo = new MovieRepository(context);

                TMDBRepository TMDBRepo = new TMDBRepository();

                var movie = await TMDBRepo.GetByID(extID);

                newMovie.ExtID = movie.ExtID;
                newMovie.Title = movie.Title;
                newMovie.Link = $"https://www.themoviedb.org/movie/{newMovie.ExtID}-{newMovie.Title}";
                newMovie.Description = movie.Overview;
                newMovie.AddedBy = userID;


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