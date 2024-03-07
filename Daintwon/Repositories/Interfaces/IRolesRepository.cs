using Daintwon.Models;

namespace Daintwon.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Roles FindById(long id);
        IEnumerable<Roles> GetAllRoles();
        void Save(Roles role);
    }
}
