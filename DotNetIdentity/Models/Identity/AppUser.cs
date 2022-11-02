using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public TwoFactorType TwoFactorType { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public String? FirstName {get; set;} = string.Empty;
        public String? LastName {get; set;} = string.Empty;
        public bool IsMfaForce {get; set;}
        public bool IsLdapLogin {get; set;}
        public string? Department { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public bool IsEnabled {get; set;}
    }
}