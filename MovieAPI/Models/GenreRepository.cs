using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        private readonly RepositoryContext _dbContext;

        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }


        public string GetNameByID(int id)
        {

            GenreRepository genreRepo = new GenreRepository(_dbContext);
            Console.WriteLine();
            

            return genreRepo.GetByCondition(g => g.Id == id).ToString();

        }
    }
}
