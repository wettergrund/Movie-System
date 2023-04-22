using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {

        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        //public List<Genre> GetGenre { get; set; }


        public string GetNameByID(int id)
        {
            //Return genre name by DB ID.

            RepositoryContext context = new RepositoryContext();

            GenreRepository genreRepo = new GenreRepository(context);
            var genres = genreRepo.GetByCondition(g => g.Id == id).ToList();

            return genres.First().Title;

        }


    }
}
