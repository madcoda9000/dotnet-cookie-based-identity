using Microsoft.AspNetCore.Identity;

namespace DotNetIdentity.IdentitySettings
{
    /// <summary>
    /// class to override IdentityErrorDescriber IdentotyError messages
    /// </summary>
    public class ErrorDescriber : IdentityErrorDescriber
    {       
        /// <summary>
        /// custom IdentityError if password contains username
        /// </summary>
        /// <returns>IdentityError</returns>
        public static IdentityError PasswordContainsUsername() => new() { Code = "PasswordContainsUsername", Description = "Password can not contains user name." };
        /// <summary>
        /// custom IdentityError if username length is too short
        /// </summary>
        /// <returns>IdentityError</returns>
        public static IdentityError UserNameLength() => new() { Code = "UserNameLength", Description = "User name must be at least 6 characters." };
        /// <summary>
        /// custom IdentityError if username contains user email
        /// </summary>
        /// <returns>IdentityError</returns>
        public static IdentityError UserNameContainsEmail() => new() { Code = "UserNameContainsEmail", Description = "User name can not contains email name." };
        /// <summary>
        /// custom identity error if password is empty
        /// </summary>
        /// <returns>IdentityError</returns>
        public static IdentityError PasswordIsEmpty() => new() { Code = "PasswordIsEmpty", Description = "Password should not be an empty strign!." };
    }
}