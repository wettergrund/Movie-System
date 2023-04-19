using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;
using System.Linq.Expressions;

namespace MovieAPI.Models
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public IQueryable<T> GetAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition) =>
            RepositoryContext.Set<T>().Where(condition).AsNoTracking();
        public void Create(T item) => RepositoryContext.Set<T>().Add(item);
        public void Update(T item) => RepositoryContext.Set<T>().Update(item);
        public void Delete(T item) => RepositoryContext.Set<T>().Remove(item);



    }
}
