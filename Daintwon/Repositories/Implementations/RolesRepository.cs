using Daintwon.Models;
using Daintwon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Daintwon.Repositories.Implementations
{
    public class RolesRepository(DaintwonContext repositoryContext) : RepositoryBase<Roles>(repositoryContext), IRolesRepository
    {
        public Roles FindById(long id)
        {
            return FindByCondition(role => role.Id == id)
                .Include(role => role.User)
                .FirstOrDefault();
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return [.. FindAll().Include(role => role.User)];
        }

        public void Save(Roles role)
        {
            Create(role);
            SaveChanges();
        }
    }
}
