using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.Models.Identity
{
    /// <summary>
    /// class to define a user object
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// 2fa type property
        /// </summary>
        /// <value>TwoFactorType</value>
        public TwoFactorType TwoFactorType { get; set; }
        /// <summary>
        /// birthday property
        /// </summary>
        /// <value>DateTime</value>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// gender property
        /// </summary>
        /// <value>Gender</value>
        public Gender Gender { get; set; }
        /// <summary>
        /// birthday CreatedOn
        /// </summary>
        /// <value>DateTime</value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// firstname property
        /// </summary>
        /// <value>string</value>
        public String? FirstName { get; set; } = string.Empty;
        /// <summary>
        /// lastname property
        /// </summary>
        /// <value>string</value>
        public String? LastName { get; set; } = string.Empty;
        /// <summary>
        /// 2fa force property
        /// </summary>
        /// <value>bool</value>
        public bool IsMfaForce { get; set; }
        /// <summary>
        /// ldap login property
        /// </summary>
        /// <value>bool</value>
        public bool IsLdapLogin { get; set; }
        /// <summary>
        /// department property
        /// </summary>
        /// <value>Department</value>
        public string? Department { get; set; } = string.Empty;
        /// <summary>
        /// profile picture proerty
        /// </summary>
        /// <value>string</value>
        public string? ProfilePicture { get; set; }
        /// <summary>
        /// account enbaled property
        /// </summary>
        /// <value>bool</value>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// roles combined property
        /// </summary>
        /// <value></value>
        public string? RolesCombined { get; set; } = string.Empty;
    }
}