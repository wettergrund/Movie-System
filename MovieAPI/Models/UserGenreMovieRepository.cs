using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class UserGenreMovieRepository : RepositoryBase<UserGenreMovie>, IUserGenreMovieRepository
    {
        public UserGenreMovieRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
