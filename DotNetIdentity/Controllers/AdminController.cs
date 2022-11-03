using System.Security.Claims;
using DotNetIdentity.Models;
using DotNetIdentity.Data;
using DotNetIdentity.Models.Identity;
using DotNetIdentity.Models.ViewModels;
using DotNetIdentity.Models.DataModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;
using DatatableJS.Data;

namespace DotNetIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext _context;
        public AdminController(AppDbContext db, ILogger<AdminController> log, IConfiguration conf, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = conf;
            _logger = log;
            _context = db;
        }

        public IActionResult Index() => View();
        public IActionResult SystemLogs() => View();

        public IActionResult AuditLogs() => View();
        public IActionResult ErrorLogs() => View();

        public async Task<IActionResult> Users() => View(await _userManager.Users.ToListAsync());
        
        public async Task<IActionResult> Roles() => View(await _roleManager.Roles.ToListAsync());

        [HttpPost]
        public IActionResult GetErrorLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==false && Convert.ToInt32(l.Level) == 4).ToDataResult(request);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetAppLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==false && Convert.ToInt32(l.Level) < 4).ToDataResult(request);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetAuditLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==true).ToDataResult(request);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetUsers(DataRequest request) {
            DataResult<AppUser> result = _userManager.Users.ToDataResult(request);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetUsersVanillaDatatableJs()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from us in _userManager.Users select us);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if(sortColumn!.ToLower()=="email") {
                        customerData = customerData.OrderBy(u=>u.Email);
                    } else if(sortColumn.ToLower()=="username") {
                        customerData = customerData.OrderBy(u=>u.Email);
                    }                    
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName!.Contains(searchValue) 
                                                || m.LastName!.Contains(searchValue) 
                                                || m.UserName.Contains(searchValue) 
                                                || m.Email.Contains(searchValue) );
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> NewUser() {
            var mod = new NewUserModel();
            mod.Roles = await _roleManager.Roles.Select(s => new AssignRoleViewModel
            {
                RoleId = s.Id,
                RoleName = s.Name,
                IsAssigned = false
            }).ToListAsync();

            return View(mod);
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(NewUserModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser();
                user.IsEnabled = true;
                user.IsMfaForce = viewModel.IsMfaForce;
                user.IsLdapLogin = viewModel.IsLdapLogin;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.EmailConfirmed = true;
                user.Gender = viewModel.Gender;
                user.Department = viewModel.Department.ToString();
                user.UserName = viewModel.UserName;

                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    // roles
                    foreach (var item in viewModel.Roles)
                    {
                        if (item.IsAssigned)
                        {
                            await _userManager.AddToRoleAsync(user, item.RoleName);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                        }
                    }
                }

                ViewData["message"] = "User created success fully.";
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " created new user " + user.UserName);
            }
            return View(new NewUserModel());
        }


        public async Task<IActionResult> UpsertRole(string? id)
        {
            if (id != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                return View(role.Adapt<UpsertRoleViewModel>());
            }
            
            return View(new UpsertRoleViewModel());
        }

        [HttpGet]
        public async Task<ActionResult> DisableUser(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsEnabled = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpGet]
        public async Task<ActionResult> EnableUser(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsEnabled = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpGet]
        public async Task<ActionResult> EnableLdapLogin(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsLdapLogin = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled LDAP login for user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpGet]
        public async Task<ActionResult> DisableLdapLogin(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsLdapLogin = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled ldap login for user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpGet]
        public async Task<ActionResult> EnableMfaForce(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsMfaForce = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled mfa enforce for user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpGet]
        public async Task<ActionResult> DisableMfaForce(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsMfaForce = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled mfa enforce for user " + user.UserName);
            return Json(new {success = true});
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRole(UpsertRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isUpdate = viewModel.Id != null;

                var role = isUpdate ? await _roleManager.FindByIdAsync(viewModel.Id) : new AppRole() { Name = viewModel.Name, CreatedOn = DateTime.Now };

                if (isUpdate)
                {
                    role.Name = viewModel.Name;
                }

                var result = isUpdate ? await _roleManager.UpdateAsync(role) : await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {                    
                    if(isUpdate) {
                        _logger.LogInformation("AUDIT: " + User.Identity!.Name + " changed name of " + viewModel.Id + " to " + viewModel.Name);
                    } else {
                        _logger.LogInformation("AUDIT: " + User.Identity!.Name + " created role " + viewModel.Name);
                    }
                    return RedirectToAction("Roles");
                }
                result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                result.Errors.ToList().ForEach(f => TempData["Error"] += f.Description);
            }
            
            //return View(viewModel);
            //return View("Roles", await _roleManager.Roles.ToListAsync());
            return RedirectToAction("Roles");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null) {
                var result = await _userManager.DeleteAsync(user);
                if(!result.Succeeded) {
                    result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            } else {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " deleted user " + id);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role not found.");
            }
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " deleted role " + id);
            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Users");
            }
            var viewModel = user.Adapt<EditUserViewModel>();

            var userRoles = await _userManager.GetRolesAsync(user);
            viewModel.Roles = await _roleManager.Roles.Select(s => new AssignRoleViewModel
            {
                RoleId = s.Id,
                RoleName = s.Name,
                IsAssigned = userRoles.Any(a => a == s.Name)
            }).ToListAsync();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var departmentClaim = userClaims.FirstOrDefault(f => f.Type == "Department");
            if (departmentClaim != null)
            {
                viewModel.Department = Enum.Parse<Department>(departmentClaim.Value);
            }
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                if (user != null)
                {
                    if (user.PhoneNumber != viewModel.PhoneNumber && _userManager.Users.Any(a => a.PhoneNumber == viewModel.PhoneNumber))
                    {
                        ModelState.AddModelError(string.Empty, "Phone number already in use.");
                    }
                    else
                    {
                        user.FirstName = viewModel.FirstName;
                        user.LastName = viewModel.LastName;
                        user.UserName = viewModel.UserName;
                        user.Email = viewModel.Email;
                        user.PhoneNumber = viewModel.PhoneNumber;
                        user.Gender = viewModel.Gender;
                        user.Department = viewModel.Department.ToString();

                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            // Roles
                            foreach (var item in viewModel.Roles)
                            {
                                if (item.IsAssigned)
                                {
                                    await _userManager.AddToRoleAsync(user, item.RoleName);
                                }
                                else
                                {
                                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                                }
                            }

                            // Claims
                            var userClaims = await _userManager.GetClaimsAsync(user);
                            var departmentClaim = userClaims.FirstOrDefault(a => a.Type == "Department");
                            var claimsToAdd = new Claim("Department", Enum.GetName(viewModel.Department)!);
                            if (departmentClaim != null)
                            {
                                await _userManager.ReplaceClaimAsync(user, departmentClaim, claimsToAdd);
                            }
                            else
                            {
                                await _userManager.AddClaimAsync(user, claimsToAdd);
                            }
                            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified user " + user.UserName);
                            return RedirectToAction("Users");
                        }
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
    }
}