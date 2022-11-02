using System.Text.Encodings.Web;

namespace DotNetIdentity.IdentitySettings
{
    public class TwoFactorAuthenticationService
    {
        private readonly UrlEncoder _urlEncoder;
        private readonly IConfiguration _configuration;

        public TwoFactorAuthenticationService(IConfiguration cnf, UrlEncoder urlEncoder)
        {
            _urlEncoder = urlEncoder;
            _configuration = cnf;
        }

        public string GenerateQrCodeUri(string email, string authenticatorKey)
        {
            var encodedUrl = _urlEncoder.Encode(_configuration.GetSection("AppSettings")["ApplicationName"]);
            var encodedEmail = _urlEncoder.Encode(email);

            return $"otpauth://totp/{encodedUrl}:{encodedEmail}?secret={authenticatorKey}&issuer={encodedUrl}&digits=6";
        }
    }
}