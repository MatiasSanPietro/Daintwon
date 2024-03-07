using Daintwon.Models;
using Daintwon.Models.DTOs;

namespace Daintwon.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(UserRegisterDTO user);
        List<UserDTO> GetAllUsers();
        UserDTO GetCurrentUser(string email);
        UserDTO GetUserById(long id);

    }
}
