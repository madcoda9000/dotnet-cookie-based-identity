using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings
{
    public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName) => new() { Code = "InvalidUserName", Description = $"\"{userName}\" kullanıcı adı geçersiz." };

        public override IdentityError DuplicateEmail(string email) => new() { Code = "DuplicateEmail", Description = $"\"{email}\" adresi kullanımdadır." };

        public override IdentityError PasswordTooShort(int length) => new() { Code = "PasswordTooShort", Description = $"Şifre en az {length} karakter olmalıdır." };

        // Custom Errors
        public static IdentityError PasswordContainsUsername() => new() { Code = "PasswordContainsUsername", Description = "Password can not contains user name." };
        public static IdentityError UserNameLength() => new() { Code = "UserNameLength", Description = "User name must be at least 6 characters." };
        public static IdentityError UserNameContainsEmail() => new() { Code = "UserNameContainsEmail", Description = "User name can not contains email name." };
    }
}