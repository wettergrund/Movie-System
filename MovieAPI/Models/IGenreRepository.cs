using System.Linq.Expressions;

namespace MovieAPI.Models
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        string GetNameByID(int id);
    }
}
