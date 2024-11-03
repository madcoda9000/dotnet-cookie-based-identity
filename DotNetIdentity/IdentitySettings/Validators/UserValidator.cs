using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings.Validators
{
    /// <summary>
    /// class to validate IdentityUser
    /// </summary>
    public class UserValidator : IUserValidator<AppUser>
    {
        /// <summary>
        /// Task to validate IdentityUser
        /// </summary>
        /// <param name="manager">UserManager</param>
        /// <param name="user">AppUser</param>
        /// <returns>Task of type IdentiyResult</returns>
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            if (user.UserName!.Length < 6)
            {
                errors.Add(ErrorDescriber.UserNameLength());
            }
            /*
            if (user.Email.Substring(0, user.Email.IndexOf("@")) == user.UserName)
            {
                errors.Add(ErrorDescriber.UserNameContainsEmail());
            }
            */
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}