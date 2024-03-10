using Daintwon.Models;
using Daintwon.Models.DTOs;
using Daintwon.Repositories.Interfaces;
using Daintwon.Services.Interfaces;
using Daintwon.Utils;
using Daintwon.Utils.Interfaces;

namespace Daintwon.Services.Implementations
{
    public class UserService(IUserRepository userRepository, IRolesRepository rolesRepository, IPasswordHasher passwordHasher) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public class UserServiceException(string message) : Exception(message)
        {
        }

        public User CreateUser(UserRegisterDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                throw new UserServiceException("all data is required");

            if (!EmailValidation.IsValidEmail(user.Email))
            {
                throw new UserServiceException("email not valid");
            }

            if (user.Password.Length > 8)
            {
                throw new UserServiceException("password must be at least 8 characters");
            }

            string hashedPassword = _passwordHasher.HashPassword(user.Password, out string salt);

            var newUser = new User()
            {
                Email = user.Email,
                Password = hashedPassword,
                Salt = salt,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            _userRepository.Save(newUser);

            if (user.Email.ToLowerInvariant().Split('@')[1].Contains("daintwon"))
            {
                var adminRole = new Roles()
                {
                    RoleName = "Admin",
                    User = newUser
                };

                newUser.Roles = [adminRole];
                _rolesRepository.Save(adminRole);
            }
            else
            {
                var clientRole = new Roles()
                {
                    RoleName = "Client",
                    User = newUser
                };

                newUser.Roles = [clientRole];
                _rolesRepository.Save(clientRole);
            }
            return newUser;
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
            var user = _userRepository.FindByEmail(email);

            if (user == null)
            {
                throw new UserServiceException("users email not found");
            }

            var userDTO = new UserDTO(user);
            return userDTO;
        }

        public UserDTO GetUserById(long id)
        {
            var user = _userRepository.FindById(id);

            if (user == null)
            {
                throw new UserServiceException("user doesn't exists");
            }

            var userDTO = new UserDTO(user);
            return userDTO;
        }
    }
}
