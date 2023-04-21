using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class UserMovieRepository : RepositoryBase<UserMovie>, IUserMovieRepository
    {
        public UserMovieRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
