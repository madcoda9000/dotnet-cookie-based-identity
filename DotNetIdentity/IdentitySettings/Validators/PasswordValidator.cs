using System.Diagnostics.CodeAnalysis;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings.Validators
{
    /// <summary>
    /// class to validate a password
    /// </summary>
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        /// <summary>
        /// Task to validate a password
        /// </summary>
        /// <param name="manager">UserManager</param>
        /// <param name="user">AppUser</param>
        /// <param name="password">string</param>
        /// <returns>Task of type IdentityResult</returns>
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if(String.IsNullOrEmpty(password))
            {
                errors.Add(ErrorDescriber.PasswordIsEmpty());
            }

            if (user.UserName == password)
            {
                errors.Add(ErrorDescriber.PasswordContainsUsername());
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}