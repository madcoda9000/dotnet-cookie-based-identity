using System;
using DotNetIdentity.Models.Identity;

namespace DotNetIdentity.Models.ViewModels
{
	public class NewUserModel
	{
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Gender Gender { get; set; } = Gender.Unknown;
        public Department Department { get; set; } = Department.Software;
        public List<AssignRoleViewModel> Roles { get; set; } = new();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsLdapLogin { get; set; }
        public bool IsMfaForce { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

