using Daintwon.Models;
using Daintwon.Models.DTOs;
using Daintwon.Repositories.Implementations;
using Daintwon.Repositories.Interfaces;
using Daintwon.Services.Interfaces;

namespace Daintwon.Services.Implementations
{
    public class UserService(IUserRepository userRepository, IRolesRepository rolesRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        public class UserServiceException(string message) : Exception(message)
        {
        }

        public User CreateUser(UserRegisterDTO user)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new UserDTO(user));
            }

            return usersDTO;
        }

        public UserDTO GetCurrentUser(string email)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
