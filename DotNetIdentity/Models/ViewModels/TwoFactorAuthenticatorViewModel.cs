namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// model for TwoFactorAuthenticator view
    /// </summary>
    public class TwoFactorAuthenticatorViewModel
    {
        /// <summary>
        /// the shared key
        /// </summary>
        /// <value>string</value>
        public string SharedKey { get; set; } = default!;
        /// <summary>
        /// the auth uri
        /// </summary>
        /// <value>string</value>
        public string AuthenticationUri { get; set; } = default!;
        /// <summary>
        /// the otp code
        /// </summary>
        /// <value>string</value>
        public string VerificationCode { get; set; } = default!;
    }
}