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
        /// Controller action for SettingsLdap view
        /// </summary>
        /// <returns>view SettingsLdap</returns>
        public ActionResult SettingsLdap(){
            var viewModel = new LdapSettings();
            viewModel = _settings.Ldap;
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for SettingsMail view
        /// </summary>
        /// <returns>view SettingsMail</returns>
        public ActionResult SettingsMail(){
            var viewModel = new MailSettings();
            viewModel = _settings.Mail;
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for SettingsApp view
        /// </summary>
        /// <returns>view SettingsApp</returns>
        public ActionResult SettingsApp(){
            var viewModel = new GlobalSettings();
            viewModel = _settings.Global;
            return View(viewModel);
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
                ViewData["message"] = "Settings successfully saved!";
                return View();       
        }

        /// <summary>
        /// controller POST method to save Application settings
        /// </summary>
        /// <param name="viewModel">GlobalSettings</param>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SettingsMail(GlobalSettings viewModel) {
   
                _settings.Global.ApplicationName = viewModel.ApplicationName;
                _settings.Global.SessionCookieExpiration = viewModel.SessionCookieExpiration;
                _settings.Global.SessionTimeoutWarnAfter = viewModel.SessionTimeoutWarnAfter;
                _settings.Global.SessionTimeoutRedirAfter = viewModel.SessionTimeoutRedirAfter;
                await _settings.Save();
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Application settings! ");
                ViewData["message"] = "Settings successfully saved!";
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
                _settings.Ldap.LdapDomainController = viewModel.LdapDomainName;
                _settings.Ldap.LdapBaseDn = viewModel.LdapBaseDn;
                _settings.Ldap.LdapEnabled = viewModel.LdapEnabled;
                _settings.Ldap.LdapGroup = viewModel.LdapGroup;
                await _settings.Save();
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified Ldap settings! ");
                ViewData["message"] = "Settings successfully saved!";
                return View();       
        }
    }
}