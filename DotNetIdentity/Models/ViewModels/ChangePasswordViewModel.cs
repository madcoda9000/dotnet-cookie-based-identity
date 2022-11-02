using System.ComponentModel.DataAnnotations;

namespace DotNetIdentity.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; } = default!;
        public string NewPassword { get; set; } = default!;

        [Compare(nameof(NewPassword), ErrorMessage = "Entered passwords doesn't match.")]
        public string NewPasswordConfirm { get; set; } = default!;
    }
}