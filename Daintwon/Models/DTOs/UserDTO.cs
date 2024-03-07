using System.Text.Json.Serialization;

namespace Daintwon.Models.DTOs
{
    public class UserDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<RolesDTO> Roles { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Roles = user.Roles?.Select(role => new RolesDTO(role)).ToList();
        }
        public UserDTO() { }
    }
}
