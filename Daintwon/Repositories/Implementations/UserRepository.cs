using Daintwon.Models;
using Daintwon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Daintwon.Repositories.Implementations
{
    public class UserRepository(DaintwonContext repositoryContext) : RepositoryBase<User>(repositoryContext), IUserRepository
    {
        public User FindByEmail(string email)
        {
            return FindByCondition(user => user.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .Include(user => user.Roles)
                .FirstOrDefault();
        }

        public User FindById(long id)
        {
            return FindByCondition(user => user.Id == id)
                .Include(user => user.Roles)
                .FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return [.. FindAll().Include(user => user.Roles)];
        }

        public void Save(User user)
        {
            var dbUser = FindByEmail(user.Email);

            if (dbUser != null)
            {
                Update(user);
            }
            else
            {
                Create(user);
            }
            SaveChanges();
        }
    }
}
