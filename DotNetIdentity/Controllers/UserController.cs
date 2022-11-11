using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DotNetIdentity.Helpers;
using DotNetIdentity.IdentitySettings;
using DotNetIdentity.Models.Identity;
using DotNetIdentity.Models.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Asn1;

namespace DotNetIdentity.Controllers
{
    /// <summary>
    /// Controller Class for User views
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Property of type UserManager
        /// </summary>
        private readonly UserManager<AppUser> _userManager;
        /// <summary>
        /// Property of type SignInManager
        /// </summary>
        private readonly SignInManager<AppUser> _signInManager;
        /// <summary>
        /// Property of type IWebHostEnviroment
        /// </summary>
        private readonly IWebHostEnvironment _hostEnvironment;
        /// <summary>
        /// Property of type EmailHelper
        /// </summary>
        private readonly EmailHelper _emailHelper;
        /// <summary>
        /// Property of type TwoFactorAUthenticationService
        /// </summary>
        private readonly TwoFactorAuthenticationService _twoFactorAuthService;
        /// <summary>
        /// Property of type ILogger
        /// </summary>
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// Property of type IStringLocalizer
        /// </summary>
        private readonly IStringLocalizer<UserController> _localizer;
        /// <summary>
        /// property of type ISettingsService
        /// </summary>
        private readonly ISettingsService _sett;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="sett">type ISettingsService</param>
        /// <param name="localizer">type IStringLocalizer</param>
        /// <param name="log">type ILogger</param>
        /// <param name="webhostEnviroment">type IwebHostEnviroment</param>
        /// <param name="userManager">type UserManager</param>
        /// <param name="signInManager">type SIgnInManager</param>
        /// <param name="twoFactorAuthService">type TwoFactorAuthService</param>
        /// <param name="emailHelper">työe EmailHelper</param>
        public UserController(ISettingsService sett, IStringLocalizer<UserController> localizer, ILogger<UserController> log, IWebHostEnvironment webhostEnviroment, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TwoFactorAuthenticationService twoFactorAuthService, EmailHelper emailHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailHelper = emailHelper;
            _twoFactorAuthService = twoFactorAuthService;
            _hostEnvironment = webhostEnviroment;
            _logger = log;
            _localizer = localizer;
            _sett = sett;
        }

        /// <summary>
        /// Controller action for Register view
        /// </summary>
        /// <returns>view Register</returns>
        public IActionResult Register() => View();

        /// <summary>
        /// Controller action for DebugUserInfo view
        /// </summary>
        /// <returns>view DebugUserInfo</returns>
        [Authorize]
        public IActionResult debugUserInfo() => View();

        /// <summary>
        /// Controller action for Ping view
        /// </summary>
        /// <returns>view Ping</returns>
        [Authorize]
        public IActionResult ping() => View();

        /// <summary>
        /// Controller POST method for Register view
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Redirect to action Login </returns>
        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender,
                    BirthDay = viewModel.BirthDay,
                    Department = viewModel.Department.ToString(),
                    TwoFactorType = Models.TwoFactorType.None,
                    CreatedOn = DateTime.UtcNow,
                    IsEnabled = true
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "User", new
                    {
                        userId = user.Id,
                        token = confirmationToken
                    }, HttpContext.Request.Scheme);

                    await _emailHelper.SendAsync(new()
                    {
                        Subject = "Confirm e-mail",
                        Body = $"Please <a href='{confirmationLink}'>click</a> to confirm your e-mail address.",
                        To = user.Email
                    });
                    _logger.LogInformation("AUDIT: " + "registered successfully new user: " + user.UserName);
                    return RedirectToAction("Login");
                }
                result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
            }
            return View(viewModel);
        }
        
        /// <summary>
        /// Controller action for Login view
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>view Login</returns>
        public async Task<IActionResult> Login(string? returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        /// \todo Implement Ldap Login
        /// <summary>
        /// Controller POST method for Login view
        /// </summary>
        /// <returns>Redirect to action</returns>
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = new Microsoft.AspNetCore.Identity.SignInResult();

                    if(user.IsLdapLogin==true) {
                        bool ldapAuthSuccess = false;
                        try {
                            ldapAuthSuccess = this.makeLDAPAuth(user, viewModel.Password);
                        } catch (Exception ex) {
                            ModelState.AddModelError(string.Empty, ex.Message);
                        } 
                        if(ldapAuthSuccess==true) {
                            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var res1 = _userManager.ResetPasswordAsync(user, code, viewModel.Password);
                            result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);
                            if(result.Succeeded==true) {
                                _logger.LogWarning("AUDIT: " + user.UserName + " LDAP login in successful.");
                            }
                        }                        
                    } else {
                        result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);
                        if(result.Succeeded==true) {
                            _logger.LogWarning("AUDIT: " + user.UserName + " DB login successful.");
                        } 
                    }                    
                    
                    if(result.Succeeded && user.IsEnabled==false) {
                        await _signInManager.SignOutAsync();
                        _logger.LogWarning("AUDIT: " + user.UserName + " logged in successfully. But account is disabled!");
                        ModelState.AddModelError(string.Empty, _localizer["1"]);
                    }
                    else if(result.Succeeded && user.IsMfaForce==true) {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        _logger.LogInformation("AUDIT: " + user.UserName + " logged in successfully. But MFA is enforced. Redirecting to action: EnforceTwoFactorAuthenticator");
                        return RedirectToAction("EnforceTwoFactorAuthenticator");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        _logger.LogInformation("AUDIT: " + user.UserName + " logged in successfully. But MFA is enabled. Redirecting to action: TwoFactorLogin");
                        return RedirectToAction("TwoFactorLogin", new { ReturnUrl = TempData["ReturnUrl"] });
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutEndUtc = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutEndUtc.Value - DateTime.UtcNow;
                        _logger.LogWarning("AUDIT: " + user.UserName + " logged in successfully. But Account is locked out for at least another " + timeLeft.Minutes + "minutes!");
                        ModelState.AddModelError(string.Empty, _localizer["2", timeLeft.Minutes.ToString()]);
                    }
                    else if (result.IsNotAllowed)
                    {
                        _logger.LogWarning("AUDIT: " + user.UserName + " logged in successfully. But email is not confirmed!");
                        ModelState.AddModelError(string.Empty, _localizer["3"]);
                    }
                    else if(result.Succeeded && user.IsMfaForce==false && !result.RequiresTwoFactor && user.IsEnabled) {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        var returnUrl = TempData["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl.ToString() ?? "/");
                        }
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        _logger.LogWarning("AUDIT: " + user.UserName + " login in failed. Invalid username or password!");
                        ModelState.AddModelError(string.Empty, _localizer["4"]);
                    }
                }
                else
                {
                    _logger.LogWarning("AUDIT: " + "Login in failed. Invalid username or password!");
                    ModelState.AddModelError(string.Empty, _localizer["4"]);
                }
            }
            return View(viewModel);
        }

        /// <summary>
        /// Controller action for view TwoFactorLogin
        /// </summary>
        /// <param name="returnUrl">optional return url as string</param>
        /// <returns>view TwoFactorLogin</returns>
        public async Task<IActionResult> TwoFactorLogin(string? returnUrl)
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if(user.TwoFactorType == DotNetIdentity.Models.TwoFactorType.Email) {
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await _emailHelper.SendAsync(new()
                    {
                        Subject = "Your 2fa code",
                        Body = $"Please use this " + token + " 2fa code to verify your login.",
                        To = user.Email
                    });
                    _logger.LogInformation("AUDIT: send otp code to " + user.Email);
            }

            return View(new TwoFactorLoginVieWModel
            {
                TwoFactorType = user.TwoFactorType,
            });
        }

        /// <summary>
        /// controller POST method for TwoFactorLogin view
        /// </summary>
        /// <param name="vieWModel">tyle TwoFactorLoginVieWModel</param>
        /// <returns>rediret to return url</returns>
        [HttpPost]
        public async Task<IActionResult> TwoFactorLogin(TwoFactorLoginVieWModel vieWModel)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user.TwoFactorType == Models.TwoFactorType.Authenticator)
            {
                var result = vieWModel.IsRecoveryCode ? await _signInManager.TwoFactorRecoveryCodeSignInAsync(vieWModel.VerificationCode) : await _signInManager.TwoFactorAuthenticatorSignInAsync(vieWModel.VerificationCode, true, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("AUDIT: " + user.UserName + "Authenticator 2fa verified successfully");
                    return Redirect(TempData["ReturnUrl"]?.ToString() ?? "/");
                }
                _logger.LogWarning("AUDIT: " + user.UserName + "Authenticator 2fa verification failed. Invalid otp code");
                ModelState.AddModelError(string.Empty, _localizer["5"]);
            }
            else if (user.TwoFactorType == Models.TwoFactorType.Email)
            {
                var result = vieWModel.IsRecoveryCode ? await _signInManager.TwoFactorRecoveryCodeSignInAsync(vieWModel.VerificationCode) : await _signInManager.TwoFactorSignInAsync("Email", vieWModel.VerificationCode, true, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("AUDIT: " + user.UserName + "Email 2fa verified successfully");
                    return Redirect(TempData["ReturnUrl"]?.ToString() ?? "/");
                }
                _logger.LogWarning("AUDIT: " + user.UserName + "Email 2fa verification failed. Invalid otp code");
                ModelState.AddModelError(string.Empty, _localizer["5"]);
            }

            return View(vieWModel);
        }

        /// <summary>
        /// controller action for Logout
        /// </summary>
        /// <returns></returns>
        public async Task Logout() => await _signInManager.SignOutAsync();

        /// <summary>
        /// controller action for Profile view
        /// </summary>
        /// <returns>view Profile</returns>
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var me = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (me == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            var mod = new UpdateProfileViewModel();
            mod.Email = me.Email;
            mod.BirthDay = me.BirthDay;
            mod.UserName = me.UserName;
            mod.PhoneNumber = me.PhoneNumber;
            mod.Gender = me.Gender;            
            //return View(me.Adapt<UpdateProfileViewModel>());
            return View(mod);
        }

        /// <summary>
        /// controller POST methood for Profile view
        /// </summary>
        /// <param name="viewModel">type UpdateProfileViewModel</param>
        /// <returns>redirect to action Index</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(UpdateProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var me = await _userManager.FindByNameAsync(User.Identity?.Name);
                if (me != null)
                {
                    if (me.PhoneNumber != viewModel.PhoneNumber && _userManager.Users.Any(a => a.PhoneNumber == viewModel.PhoneNumber))
                    {
                        ModelState.AddModelError(string.Empty, _localizer["6"]);
                    }
                    else
                    {
                        if(viewModel.ProfilePicture!=null) {
                            using (var memoryStream = new MemoryStream())
                            {
                                await viewModel.ProfilePicture.CopyToAsync(memoryStream);

                                // Upload the file if less than 2 MB
                                if (memoryStream.Length < 2097152)
                                {
                                    var format = "image/png";
                                    var Content = memoryStream.ToArray();
                                    var imageData = $"data:{format};base64,{Convert.ToBase64String(Content)}";
                                    me.ProfilePicture = imageData;

                                }
                                else
                                {
                                    ModelState.AddModelError("File", _localizer["7"]);
                                }
                            }
                        } else {
                            me.ProfilePicture = null;
                        }                     

                        me.UserName = viewModel.UserName;
                        me.Email = viewModel.Email;
                        me.PhoneNumber = viewModel.PhoneNumber;
                        me.Gender = viewModel.Gender;
                        me.BirthDay = viewModel.BirthDay;
                        //me.Department = viewModel.Department.ToString();

                        var result = await _userManager.UpdateAsync(me);
                        if (result.Succeeded)
                        {
                            await _userManager.UpdateSecurityStampAsync(me);
                            await _signInManager.SignOutAsync();
                            await _signInManager.SignInAsync(me, true);

                            _logger.LogInformation("AUDIT: " + me.UserName + "successfully updated own user profile");

                            return RedirectToAction("Index", "Home");
                        }
                        result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                    }
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
        }

        /// <summary>
        /// controller action for ChangePassword view
        /// </summary>
        /// <returns>view ChangePassword</returns>
        [Authorize]
        public IActionResult ChangePassword() => View();

        /// <summary>
        /// controller POST method for ChangePassword view
        /// </summary>
        /// <param name="viewModel">ChangePasswordViewModel</param>
        /// <returns>redirect to action Index</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var me = await _userManager.FindByNameAsync(User.Identity?.Name);

                var passwordValid = await _userManager.CheckPasswordAsync(me, viewModel.Password);
                if (passwordValid)
                {
                    var result = await _userManager.ChangePasswordAsync(me, viewModel.Password, viewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(me);

                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(me, true);
                        _logger.LogInformation("AUDIT: " + me.UserName + " successfully changed own password!");

                        return RedirectToAction("Index", "Admin");
                    }
                    _logger.LogWarning("AUDIT: " + me.UserName + "tried to change password. Error: " + result.Errors.FirstOrDefault()!.ToString());
                    result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
                else
                {
                    _logger.LogWarning("AUDIT: " + me.UserName + " tried to change password. But provided invalid current pasword!");
                    ModelState.AddModelError(string.Empty, _localizer["8"]);
                }
            }

            return View();
        }

        /// <summary>
        /// controller action for ForgotPassword view
        /// </summary>
        /// <returns>view ForgotPassword</returns>
        public IActionResult ForgotPassword() => View();

        /// <summary>
        /// controller POST method for ForgotPassword view
        /// </summary>
        /// <param name="viewModel">ForgotPasswordViewModel</param>
        /// <returns>redirect to action Index</returns>
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user != null)
                {
                    var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordLink = Url.Action("ResetPassword", "User", new
                    {
                        userId = user.Id,
                        token = passwordResetToken
                    }, HttpContext.Request.Scheme);

                    await _emailHelper.SendAsync(new()
                    {
                        Subject = "Reset password",
                        Body = $"Please <a href='{passwordLink}'>click</a> to reset your password.",
                        To = user.Email
                    });
                    _logger.LogWarning("AUDIT: " + user.UserName + " requested a password reset!");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _localizer["9"]);
                }
            }
            return View(viewModel);
        }

        /// <summary>
        /// controller action for ResetPassword view
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="token">the reset token</param>
        /// <returns>view ResetPassword</returns>
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View(new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token
            });
        }

        /// <summary>
        /// controller POST method for ResetPassword view
        /// </summary>
        /// <param name="viewModel">type ResetPasswordViewModel</param>
        /// <returns>redirect to action Login</returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        _logger.LogInformation("AUDIT: " + user.UserName + "successfully resetted own password.");
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(viewModel);
        }

        /// <summary>
        /// controller action for ConfirmEmail view
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="token">the confirmation token</param>
        /// <returns>redirect to action Index</returns>
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _logger.LogInformation("AUDIT: " + user.UserName + " successfully confirmed email addres!");
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// controller action for view TwiFactorType
        /// </summary>
        /// <returns>view TwoFactorTaype</returns>
        [Authorize]
        public async Task<IActionResult> TwoFactorType()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            return View(new TwoFactorTypeViewModel
            {
                TwoFactorType = user.TwoFactorType
            });
        }

        /// <summary>
        /// controller POST method for view TwoFactorType
        /// </summary>
        /// <param name="viewModel">TwoFactorTypeViewModel</param>
        /// <returns>rediorect to action TwoFactorType</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TwoFactorType(TwoFactorTypeViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            user.TwoFactorType = viewModel.TwoFactorType;
            await _userManager.UpdateAsync(user);
            await _userManager.SetTwoFactorEnabledAsync(user, user.TwoFactorType != Models.TwoFactorType.None);

            if (viewModel.TwoFactorType == Models.TwoFactorType.Authenticator)
            {
                _logger.LogInformation("AUDIT: " + user.UserName + " set authenticator as 2fa method. Redirecting to action: TwoFactorAuthenticator");
                return RedirectToAction("TwoFactorAuthenticator", "User");
            }
            _logger.LogInformation("AUDIT: " + user.UserName + " set " + user.TwoFactorType.ToString() + " as 2fa method.");
            return RedirectToAction("TwoFactorType", "User");
        }

        /// <summary>
        /// controller action for view EnforceTwoFactorAuthenticator
        /// </summary>
        /// <returns>view EnforceTwoFactorAuthenticator</returns>
        public async Task<IActionResult> EnforceTwoFactorAuthenticator()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            user.TwoFactorType = Models.TwoFactorType.Authenticator;
            await _userManager.UpdateAsync(user);

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (authenticatorKey == null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new TwoFactorAuthenticatorViewModel
            {
                SharedKey = authenticatorKey,
                AuthenticationUri = _twoFactorAuthService.GenerateQrCodeUri(user.Email, authenticatorKey)
            });
        }

        /// <summary>
        /// controller POST method for view EnforceTwoFactorAuthenticator
        /// </summary>
        /// <param name="viewModel">type TwoFactorAuthenticatorViewModel</param>
        /// <returns>redirect to action EnforceTwoFactorResult</returns>
        [HttpPost]
        public async Task<IActionResult> EnforceTwoFactorAuthenticator(TwoFactorAuthenticatorViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            
            var verificationCode = viewModel.VerificationCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var isTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
            if (isTokenValid)
            {
                if(user.IsMfaForce && user.TwoFactorEnabled==false) {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                    user.IsMfaForce = false;
                    await _userManager.UpdateAsync(user);
                }
                
                TempData["RecoveryCodes"] = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            }
            _logger.LogInformation("AUDIT: " + user.UserName + " successfully configured 2fa after enforcement. Redirecting to action: EnforceTwoFactorResult");
            return RedirectToAction("EnforceTwoFactorResult", "User");
        }

        /// <summary>
        /// controller action for view EnforceTwoFactorResult
        /// </summary>
        /// <returns>view EnforceTwoFactorResult</returns>
        public async Task<IActionResult> EnforceTwoFactorResult() {
            await _signInManager.SignOutAsync();
            return View();
        }

        /// <summary>
        /// controller action for view TwoFActorAuthenticator
        /// </summary>
        /// <returns>view TwoFActorAuthenticator</returns>
        public async Task<IActionResult> TwoFactorAuthenticator()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user.TwoFactorEnabled && user.TwoFactorType == Models.TwoFactorType.Authenticator)
            {
                var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
                if (authenticatorKey == null)
                {
                    await _userManager.ResetAuthenticatorKeyAsync(user);
                    authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
                }

                return View(new TwoFactorAuthenticatorViewModel
                {
                    SharedKey = authenticatorKey,
                    AuthenticationUri = _twoFactorAuthService.GenerateQrCodeUri(user.Email, authenticatorKey)
                });
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// controller POST method for view TwoFActorAuthenticator
        /// </summary>
        /// <param name="viewModel">type TwoFactorAuthenticatorViewModel</param>
        /// <returns>redirect to action TwoFactorType</returns>
        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthenticator(TwoFactorAuthenticatorViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            
            var verificationCode = viewModel.VerificationCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var isTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
            if (isTokenValid)
            {
                if(user.IsMfaForce && user.TwoFactorEnabled==false) {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                    user.IsMfaForce = false;
                    await _userManager.UpdateAsync(user);
                }
                
                TempData["RecoveryCodes"] = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            }
            _logger.LogInformation("AUDIT: " + user.UserName + " successfully configured 2fa!");
            return RedirectToAction("TwoFactorType", "User");
        }    

        #region "LDAP AUTHENTICATION"
        /// <summary>
        /// method to get groups for a user
        /// </summary>
        /// <param name="_ldapConnection">the active ldap connection</param>
        /// <param name="user">the username</param>
        /// <param name="LDAPBaseDn">the base dn of the ldap connection</param>
        /// <returns>a IEnumerable List oft group names</returns>
        private IEnumerable<string> GetGroupsForUserCore(LdapConnection _ldapConnection, string user, string LDAPBaseDn)
        {
            LdapSearchQueue searchQueue = _ldapConnection.Search(

                LDAPBaseDn,
                LdapConnection.ScopeSub,
                $"(sAMAccountName={user})",
                new string[] { "cn", "memberOf" },
                false,
                null as LdapSearchQueue);

            LdapMessage message;
            while ((message = searchQueue.GetResponse()) != null)
            {
                if (message is LdapSearchResult searchResult)
                {
                    LdapEntry entry = searchResult.Entry;
                    foreach (string value in HandleEntry(entry))
                        yield return value;
                }
                else
                    continue;
            }

            IEnumerable<string> HandleEntry(LdapEntry entry)
            {
                LdapAttribute attr = entry.GetAttribute("memberOf");

                if (attr == null) yield break;

                foreach (string value in attr.StringValueArray)
                {
                    string groupName = GetGroup(value);
                    yield return groupName;
                }
            }

            string GetGroup(string value)
            {
                Match match = Regex.Match(value, "^CN=([^,]*)");

                if (!match.Success) return null!;

                return match.Groups[1].Value;
            }
        }

        /// <summary>
        /// method to create a table of group memberships
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="username"></param>
        /// <param name="LDAPBaseDn"></param>
        /// <returns></returns>
        private Stack<string> createGroupsTable(LdapConnection lc, string username, string LDAPBaseDn)
        {
            // check for group membership
            var groups = new Stack<string>();
            var uniqueGroups = new HashSet<string>();


            foreach (string group in this.GetGroupsForUserCore(lc, username, LDAPBaseDn))
            {
                groups.Push(group);
            }
            return groups;
        }

        /// <summary>
        /// method to authenticate agains an ldap server
        /// </summary>
        /// <param name="_usr">the username</param>
        /// <param name="userpw">the users password</param>
        /// <returns>bool</returns>
        private bool makeLDAPAuth(AppUser _usr, string userpw)
        {

            int ldapPort = LdapConnection.DefaultPort;
            int ldapVersion = LdapConnection.LdapV3;
            String ldapHost = _sett.Ldap.LdapDomainController!;
            String loginDN = _sett.Ldap.LdapDomainName + @"\" + _usr.UserName;
            String password1 = userpw;
            LdapConnection lc = new LdapConnection();

            // connect to the server
            lc.Connect(ldapHost, ldapPort);
            var sdn = lc.GetSchemaDn();

            // authenticate to the server
            lc.Bind(ldapVersion, loginDN, password1);
            string authDN = lc.AuthenticationDn.ToString();

            if (authDN.ToString().Contains(loginDN) == true)
            {

                Stack<string> gr = createGroupsTable(lc, _usr.UserName, _sett.Ldap.LdapBaseDn!);
                bool erg = gr.Contains(_sett.Ldap.LdapGroup!);

                if (erg)
                {
                    lc.Disconnect();
                    return true;
                }
                else
                {
                    // ldap auth succes but user not in required group
                    lc.Disconnect();
                    return false;
                }
            }
            else
            {
                lc.Disconnect();
                return false;
            }
        }
        #endregion

    }
}