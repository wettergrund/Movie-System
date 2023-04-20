using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
