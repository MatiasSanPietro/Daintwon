namespace Daintwon.Models
{
    public class Roles
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
