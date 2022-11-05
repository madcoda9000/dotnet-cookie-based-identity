namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for ForgotPassword view
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// the user email
        /// </summary>
        /// <value>string</value>
        public string Email { get; set; } = default!;
    }
}