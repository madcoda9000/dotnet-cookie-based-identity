using System.ComponentModel.DataAnnotations;

namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for ChangePassword view
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// the password
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; } = default!;
        /// <summary>
        /// the new password
        /// </summary>
        /// <value>string</value>
        public string NewPassword { get; set; } = default!;
        /// <summary>
        /// the new password confirmed
        /// </summary>
        /// <value>string</value>
        [Compare(nameof(NewPassword), ErrorMessage = "Entered passwords doesn't match.")]
        public string NewPasswordConfirm { get; set; } = default!;
    }
}