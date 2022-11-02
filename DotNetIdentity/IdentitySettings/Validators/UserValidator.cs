using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings.Validators
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            if (user.UserName.Length < 6)
            {
                errors.Add(ErrorDescriber.UserNameLength());
            }
            if (user.Email.Substring(0, user.Email.IndexOf("@")) == user.UserName)
            {
                errors.Add(ErrorDescriber.UserNameContainsEmail());
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}