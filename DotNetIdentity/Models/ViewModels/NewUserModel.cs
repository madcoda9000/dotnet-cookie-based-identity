using System;
using DotNetIdentity.Models.Identity;

namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for NewUser view
    /// </summary>
	public class NewUserModel
	{
        /// <summary>
        /// the username
        /// </summary>
        /// <value>string</value>
        public string UserName { get; set; } = default!;
        /// <summary>
        /// the users email address
        /// </summary>
        /// <value>string</value>
        public string Email { get; set; } = default!;
        /// <summary>
        /// the gender
        /// </summary>
        /// <value>Gender</value>
        public Gender Gender { get; set; } = Gender.Unknown;
        /// <summary>
        /// the department
        /// </summary>
        /// <value>Department</value>
        public Department Department { get; set; } = Department.Software;
        /// <summary>
        /// the roles assigned
        /// </summary>
        /// <returns>List of type AppRole</returns>
        public List<AssignRoleViewModel> Roles { get; set; } = new();
        /// <summary>
        /// the firstname
        /// </summary>
        /// <value>string</value>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// the lastname
        /// </summary>
        /// <value>string</value>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// if user is ldap login
        /// </summary>
        /// <value>bool</value>
        public bool IsLdapLogin { get; set; }
        /// <summary>
        /// is 2fa enforced
        /// </summary>
        /// <value>bool</value>
        public bool IsMfaForce { get; set; }
        /// <summary>
        /// the password
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; } = string.Empty;
    }
}

