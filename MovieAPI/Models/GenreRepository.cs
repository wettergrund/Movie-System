using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {

        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public List<Genre> GetGenre { get; set; }


        public string GetNameByID(int id)
        {
            RepositoryContext context = new RepositoryContext();

            GenreRepository genreRepo = new GenreRepository(context);
            var test = genreRepo.GetByCondition(g => g.Id == id).ToList();




            return test.First().Title;

        }


    }
}
