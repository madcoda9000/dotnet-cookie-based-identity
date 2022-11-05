namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for ResetPassword view
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// the user id
        /// </summary>
        /// <value>string</value>
        public string UserId { get; set; } = default!;
        /// <summary>
        /// the reset token
        /// </summary>
        /// <value>string</value>
        public string Token { get; set; } = default!;
        /// <summary>
        /// the password
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; } = default!;
    }
}