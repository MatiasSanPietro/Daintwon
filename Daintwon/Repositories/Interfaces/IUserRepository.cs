using Daintwon.Models;

namespace Daintwon.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User FindByEmail(string email);
        User FindById(long id);
        IEnumerable<User> GetAllUsers();
        void Save(User user);
    }
}
