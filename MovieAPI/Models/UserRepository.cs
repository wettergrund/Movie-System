using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
