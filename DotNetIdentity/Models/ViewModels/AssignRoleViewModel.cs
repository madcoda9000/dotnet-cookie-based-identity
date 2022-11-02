namespace DotNetIdentity.Models.ViewModels
{
    public class AssignRoleViewModel
    {
        public string RoleId { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public bool IsAssigned { get; set; }
    }
}