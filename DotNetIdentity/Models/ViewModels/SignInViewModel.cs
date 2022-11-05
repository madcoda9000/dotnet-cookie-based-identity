namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for Login view
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// the username
        /// </summary>
        /// <value>string</value>
        public string UserName {get; set;} = string.Empty;
        /// <summary>
        /// the password
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; } = default!;
        /// <summary>
        /// the set cookie persisten option
        /// </summary>
        /// <value>bool</value>
        public bool RememberMe { get; set; } = true;
    }
}