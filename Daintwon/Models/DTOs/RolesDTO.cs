namespace Daintwon.Models.DTOs
{
    public class RolesDTO
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public RolesDTO(Roles role) 
        {
            Id = role.Id;
            RoleName = role.RoleName;
            RoleDescription = role.RoleDescription;
        }
        public RolesDTO() { }
    }
}
