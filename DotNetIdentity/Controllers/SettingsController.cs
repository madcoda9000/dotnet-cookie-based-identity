using System.Security.Claims;
using DotNetIdentity.Helpers;
using DotNetIdentity.IdentitySettings;
using DotNetIdentity.Models.Identity;
using DotNetIdentity.Models.ViewModels;
using DotNetIdentity.Models.BusinessModels;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using DotNetIdentity.Services.SettingsService;

namespace DotNetIdentity.Controllers
{
    /// \todo add translation
    /// <summary>
    /// controller class for settings views
    /// </summary>
    public class SettingsController : Controller {

        /// <summary>
        /// Property of type IStringLocalizer
        /// </summary>
        private readonly IStringLocalizer<SettingsController> _localizer;
        /// <summary>
        /// Property of type UserManager
        /// </summary>
        private readonly UserManager<AppUser> _userManager;
        /// <summary>
        /// Property of type SignInManager
        /// </summary>
        private readonly RoleManager<AppRole> _roleManager;
        /// <summary>
        /// Property of type IWebHostEnviroment
        /// </summary>
        private readonly IWebHostEnvironment _hostEnvironment;
        /// <summary>
        /// property of type IsettingsService
        /// </summary>
        private readonly ISettingsService _settings;
        /// <summary>
        /// property of type ILogger
        /// </summary>
        private readonly ILogger<SettingsController> _logger;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="sett">ISettingsService</param>
        /// <param name="loc">IStringLocalizer</param>
        /// <param name="usr">UserManager</param>
        /// <param name="rol">RoleManager</param>
        /// <param name="log">ILogger</param>
        /// /// <param name="env">IWebHostEnvironment</param>
        public SettingsController(ILogger<SettingsController> log, ISettingsService sett, IStringLocalizer<SettingsController> loc, UserManager<AppUser> usr, RoleManager<AppRole> rol, IWebHostEnvironment env) {
            _hostEnvironment = env;
            _userManager = usr;
            _roleManager = rol;
            _localizer = loc;
            _settings = sett;
            _logger = log;
        }

        /// <summary>
        /// action to restore the brand settings to default
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> restoreStyle () {
            _settings.Brand.ApplicationLogo = null;
            _settings.Brand.LoginBackground = null;
            _settings.Brand.ApplicationName = "YourApp";
            _settings.Brand.ColorDanger = "#f5023c";
            _settings.Brand.ColorInfo = "#2196f3";
            _settings.Brand.ColorLightBackground = "#f2f7ff";
            _settings.Brand.ColorPrimary = "#090251";
            _settings.Brand.ColorSuccess = "#00c853";
            _settings.Brand.ColorWarning = "#ff9800";
            _settings.Brand.ColorSecondary = "#f5023c";
            _settings.Brand.ColorHeadlines = "#090251";
            _settings.Brand.ColorLink = "#f5023c";
            _settings.Brand.ColorTextMuted = "#f2f7ff";
            await _settings.Save();

            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " restored Brand settings to default! ");
            ViewData["message"] = "Settings restired successfully";
            return RedirectToAction("SettingsBrand", "Settings"); ;
        }

        /// <summary>
        /// Controller action for SettingsBrand view
        /// </summary>
        /// <returns>view SettingsLdap</returns>
        public ActionResult SettingsBrand()
        {
            var viewModel = new UpdateBrandSettingsViewModel();
            viewModel.ApplicationLogo = _settings.Brand.ApplicationLogo;
            viewModel.ApplicationName = _settings.Brand.ApplicationName;
            viewModel.ColorDanger = _settings.Brand.ColorDanger;
            viewModel.ColorInfo = _settings.Brand.ColorInfo;
            viewModel.ColorLightBackground = _settings.Brand.ColorLightBackground;
            viewModel.ColorPrimary = _settings.Brand.ColorPrimary;
            viewModel.ColorSuccess = _settings.Brand.ColorSuccess;
            viewModel.ColorWarning = _settings.Brand.ColorWarning;
            viewModel.ColorSecondary = _settings.Brand.ColorSecondary;
            viewModel.ColorTextMuted = _settings.Brand.ColorTextMuted;
            viewModel.ColorLink = _settings.Brand.ColorLink;
            viewModel.ColorHeadlines = _settings.Brand.ColorHeadlines;
            viewModel.LoginBackground = _settings.Brand.LoginBackground;
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for SettingsLdap view
        /// </summary>
        /// <returns>view SettingsLdap</returns>
        [Authorize]
        public ActionResult SettingsLdap(){
            var viewModel = new LdapSettings();
            viewModel = _settings.Ldap;
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for SettingsMail view
        /// </summary>
        /// <returns>view SettingsMail</returns>
        [Authorize]
        public ActionResult SettingsMail(){
            var viewModel = new MailSettings();
            viewModel = _settings.Mail;
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for SettingsApp view
        /// </summary>
        /// <returns>view SettingsApp</returns>
        [Authorize]
        public ActionResult SettingsApp(){
            var viewModel = new GlobalSettings();
            viewModel = _settings.Global;
            return View(viewModel);
        }

        /// <summary>
        /// controller POST method to save Brand settings
        /// </summary>
        /// <param name="viewModel">BarndSettings</param>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SettingsBrand(UpdateBrandSettingsViewModel viewModel)
        {

            _settings.Brand.ApplicationName = viewModel.ApplicationName;
            _settings.Brand.ColorDanger = viewModel.ColorDanger;
            _settings.Brand.ColorInfo = viewModel.ColorInfo;
            _settings.Brand.ColorLightBackground = viewModel.ColorLightBackground;
            _settings.Brand.ColorPrimary = viewModel.ColorPrimary;
            _settings.Brand.ColorSuccess = viewModel.ColorSuccess;
            _settings.Brand.ColorWarning = viewModel.ColorWarning;
            _settings.Brand.ColorSecondary = viewModel.ColorSecondary;
            _settings.Brand.ColorLink = viewModel.ColorLink;
            _settings.Brand.ColorHeadlines = viewModel.ColorHeadlines;
            _settings.Brand.ColorTextMuted = viewModel.ColorTextMuted;

            if (viewModel.UploadedLogo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.UploadedLogo.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var format = "image/" + System.IO.Path.GetExtension(viewModel.UploadedLogo.FileName).Replace(".","");
                        var Content = memoryStream.ToArray();
                        var imageData = $"data:{format};base64,{Convert.ToBase64String(Content)}";
                        _settings.Brand.ApplicationLogo = imageData;

                    }
                    else
                    {
                        ModelState.AddModelError("File", _localizer["7"]);
                    }
                }
            }
            else
            {
                _settings.Brand.ApplicationLogo = null;
            }

            if (viewModel.UploadedLogonBackground != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.UploadedLogonBackground.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var format = "image/" + System.IO.Path.GetExtension(viewModel.UploadedLogonBackground.FileName).Replace(".", "");
                        var Content = memoryStream.ToArray();
                        var imageData = $"data:{format};base64,{Convert.ToBase64String(Content)}";
                        _settings.Brand.LoginBackground = imageData;

                    }
                    else
                    {
                        ModelState.AddModelError("File", _localizer["7"]);
                    }
                }
            }
            else
            {
                _settings.Brand.LoginBackground = null;
            }

            await _settings.Save();
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Brand settings! ");
            ViewData["message"] = _localizer["1"];
            return View();
        }

        /// <summary>
        /// controller POST method to save Mail settings
        /// </summary>
        /// <param name="viewModel">MailSettings</param>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SettingsMail(MailSettings viewModel) {
   
                _settings.Mail.SmtpServer = viewModel.SmtpServer;
                _settings.Mail.SmtpFromAddress = viewModel.SmtpFromAddress;
                _settings.Mail.SmtpPort = viewModel.SmtpPort;
                _settings.Mail.SmtpUseTls = viewModel.SmtpUseTls;
                _settings.Mail.Username = viewModel.Username;
                _settings.Mail.Password = viewModel.Password;
                await _settings.Save();
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Mail settings! ");
                ViewData["message"] = _localizer["1"];
                return View();       
        }

        /// <summary>
        /// controller POST method to save Application settings
        /// </summary>
        /// <param name="viewModel">GlobalSettings</param>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SettingsApp(GlobalSettings viewModel) {
                _settings.Global.SessionCookieExpiration = viewModel.SessionCookieExpiration;
                _settings.Global.SessionTimeoutWarnAfter = viewModel.SessionTimeoutWarnAfter;
                _settings.Global.SessionTimeoutRedirAfter = viewModel.SessionTimeoutRedirAfter;
                await _settings.Save();
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Application settings! ");
                ViewData["message"] = _localizer["1"];
                return View();       
        }

        /// <summary>
        /// controller POST method to save ldap settings
        /// </summary>
        /// <param name="viewModel">LdapSettings</param>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SettingsLdap(LdapSettings viewModel) {
   
                _settings.Ldap.LdapDomainName = viewModel.LdapDomainName;
                _settings.Ldap.LdapDomainController = viewModel.LdapDomainController;
                _settings.Ldap.LdapBaseDn = viewModel.LdapBaseDn;
                _settings.Ldap.LdapEnabled = viewModel.LdapEnabled;
                _settings.Ldap.LdapGroup = viewModel.LdapGroup;
                await _settings.Save();
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Ldap settings! ");
                ViewData["message"] = _localizer["1"];
                return View();       
        }
    }
}