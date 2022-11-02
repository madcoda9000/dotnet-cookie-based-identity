using System.Security.Claims;
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
using Microsoft.Extensions.Localization;

namespace DotNetIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly EmailHelper _emailHelper;
        private readonly TwoFactorAuthenticationService _twoFactorAuthService;
        private readonly ILogger<UserController> _logger;
        private readonly IStringLocalizer<UserController> _localizer;

        public UserController(IStringLocalizer<UserController> localizer, ILogger<UserController> log, IWebHostEnvironment webhostEnviroment, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TwoFactorAuthenticationService twoFactorAuthService, EmailHelper emailHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailHelper = emailHelper;
            _twoFactorAuthService = twoFactorAuthService;
            _hostEnvironment = webhostEnviroment;
            _logger = log;
            _localizer = localizer;
        }

        public IActionResult Register() => View();

        [Authorize]
        public IActionResult debugUserInfo() => View();

        [Authorize]
        public IActionResult ping() => View();

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

        public async Task<IActionResult> Login(string? returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);
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
                    else if (result.Succeeded && user.IsMfaForce==false)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        _logger.LogInformation("AUDIT: " + user.UserName + " logged in successfully.");
                        var returnUrl = TempData["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl.ToString() ?? "/");
                        }
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.RequiresTwoFactor)
                    {
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

        public async Task Logout() => await _signInManager.SignOutAsync();

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

        [Authorize]
        public IActionResult ChangePassword() => View();

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

        public IActionResult ForgotPassword() => View();

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

        [Authorize]
        public async Task<IActionResult> TwoFactorType()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            return View(new TwoFactorTypeViewModel
            {
                TwoFactorType = user.TwoFactorType
            });
        }

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
            return RedirectToAction("Index", "Home");
        }

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

        public async Task<IActionResult> EnforceTwoFactorResult() {
            await _signInManager.SignOutAsync();
            return View();
        }

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

        public IActionResult FacebookLogin(string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalResponse", "User", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalResponse", "User", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public IActionResult MicrosoftLogin(string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalResponse", "User", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
            return new ChallengeResult("Microsoft", properties);
        }

        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var externalLoginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
            if (externalLoginResult.Succeeded)
            {
                return Redirect(ReturnUrl);
            }

            var externalUserId = loginInfo.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var externalEmail = loginInfo.Principal.FindFirst(ClaimTypes.Email)?.Value;

            var existUser = await _userManager.FindByEmailAsync(externalEmail);
            if (existUser == null)
            {
                var user = new AppUser { Email = externalEmail };

                if (loginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    var userName = loginInfo.Principal.FindFirst(ClaimTypes.Name)?.Value;
                    if (userName != null)
                    {
                        userName = userName.Replace(' ', '-').ToLower() + externalUserId?.Substring(0, 5);
                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = user.Email;
                    }
                }
                else
                {
                    user.UserName = user.Email;
                }

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    var loginResult = await _userManager.AddLoginAsync(user, loginInfo);
                    if (loginResult.Succeeded)
                    {
                        // await SignInManager.SignInAsync(user, true);
                        await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        loginResult.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                    }
                }
                else
                {
                    createResult.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            }
            else
            {
                var loginResult = await _userManager.AddLoginAsync(existUser, loginInfo);
                if (loginResult.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, true);
                    await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    loginResult.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            }

            var errors = ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage).ToList();

            return View("Error", errors);
        }

        public async Task ClaimsPrincipalExample()
        {
            var licenceClaims = new List<Claim>
            {
                new(ClaimTypes.Name, "Burak"),
                new("LicenceType", "B"),
                new("ValidUntil", "2022-09"),
            };

            var passportClaims = new List<Claim>
            {
                new(ClaimTypes.Name, "Burak"),
                new("ValidUntil", "2042-07"),
                new(ClaimTypes.Country, "Turkey")
            };

            var licenceIdentity = new ClaimsIdentity(licenceClaims, "LicenceIdentity");
            var passportIdentity = new ClaimsIdentity(passportClaims, "PassportIdentity");

            var userPrincipal = new ClaimsPrincipal(new[] { licenceIdentity, passportIdentity });

            var authenticationProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(userPrincipal, authenticationProperties);
        }
    }
}