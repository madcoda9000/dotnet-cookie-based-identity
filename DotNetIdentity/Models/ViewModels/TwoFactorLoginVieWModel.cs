namespace DotNetIdentity.Models.ViewModels
{
    /// <summary>
    /// the model for TwoFactorLogin view
    /// </summary>
    public class TwoFactorLoginVieWModel
    {
        /// <summary>
        /// the otp code
        /// </summary>
        /// <value>string</value>
        public string VerificationCode { get; set; } = default!;
        /// <summary>
        /// bool if otp code is a recovery code
        /// </summary>
        /// <value>bool</value>
        public bool IsRecoveryCode { get; set; }
        /// <summary>
        /// the type of 2fa
        /// </summary>
        /// <value>TwoFactorType</value>
        public TwoFactorType TwoFactorType { get; set; }
    }
}