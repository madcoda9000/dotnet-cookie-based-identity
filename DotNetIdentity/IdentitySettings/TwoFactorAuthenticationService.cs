using System.Text.Encodings.Web;
using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.IdentitySettings
{
    /// <summary>
    /// class providing 2fa auth services
    /// </summary>
    public class TwoFactorAuthenticationService
    {
        /// <summary>
        /// UrlEncoder property
        /// </summary>
        private readonly UrlEncoder _urlEncoder;
        /// <summary>
        /// IConfiguration property
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// IsettingsService property
        /// </summary>
        private readonly ISettingsService _sett;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="cnf">IConfiguration</param>
        /// <param name="urlEncoder">UrlEncoder</param>
        /// <param name="sett">ISettingsService</param>
        public TwoFactorAuthenticationService(IConfiguration cnf, UrlEncoder urlEncoder, ISettingsService sett)
        {
            _urlEncoder = urlEncoder;
            _configuration = cnf;
            _sett = sett;
        }

        /// <summary>
        /// method to generate a barcode for 2fa configuration
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="authenticatorKey">string</param>
        /// <returns>string</returns>
        public string GenerateQrCodeUri(string email, string authenticatorKey)
        {
            var encodedUrl = _urlEncoder.Encode(_sett.Brand.ApplicationName!);
            var encodedEmail = _urlEncoder.Encode(email);

            return $"otpauth://totp/{encodedUrl}:{encodedEmail}?secret={authenticatorKey}&issuer={encodedUrl}&digits=6";
        }
    }
}