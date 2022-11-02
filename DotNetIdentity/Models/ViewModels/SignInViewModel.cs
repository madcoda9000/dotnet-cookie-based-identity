namespace DotNetIdentity.Models.ViewModels
{
    public class SignInViewModel
    {
        public string UserName {get; set;} = string.Empty;
        public string Password { get; set; } = default!;
        public bool RememberMe { get; set; } = true;
    }
}