namespace DotNetIdentity.Models.ViewModels
{
    public class TwoFactorLoginVieWModel
    {
        public string VerificationCode { get; set; } = default!;
        public bool IsRecoveryCode { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
    }
}