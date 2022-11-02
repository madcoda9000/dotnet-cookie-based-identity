namespace DotNetIdentity.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}