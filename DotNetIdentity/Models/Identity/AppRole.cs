using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.Models.Identity
{
    public class AppRole : IdentityRole
    {
        public DateTime CreatedOn { get; set; }
    }
}