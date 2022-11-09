global using System.Globalization;
global using Microsoft.AspNetCore.Localization;
global using DotNetIdentity.Services.SettingsService;
using System.Net;
using DotNetIdentity.Data;
using DotNetIdentity.Helpers;
using DotNetIdentity.IdentitySettings;
using DotNetIdentity.IdentitySettings.Requirements;
using DotNetIdentity.IdentitySettings.Validators;
using DotNetIdentity.Models;
using DotNetIdentity.Models.BusinessModels;
using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;

[assembly: System.Reflection.AssemblyVersion("0.0.*")]

var builder = WebApplication.CreateBuilder(args);

// add serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

// add localization 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// configure localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("de")
    };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
builder.Services.AddScoped<IAuthorizationHandler, FreeTrialExpireHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddTransient<UserManager<AppUser>>();
builder.Services.AddScoped<EmailHelper>();
builder.Services.AddScoped<TwoFactorAuthenticationService>();

// database context
if (builder.Configuration.GetSection("AppSettings").GetSection("DataBaseType").Value == "MySql")
{
    var connectionString = builder.Configuration.GetConnectionString("MySql");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);

    // add distributed mysql server session cache
    builder.Services.AddDistributedMySqlCache(options =>
    {
        options.ConnectionString = builder.Configuration.GetConnectionString("MySql");
        options.SchemaName = builder.Configuration.GetSection("AppSettings").GetSection("DataBaseName").Value;
        options.TableName = "AppSessionCache";
        options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(60);
    });
    builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "SessionCookie";
            options.IdleTimeout = TimeSpan.FromMinutes(180);
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });
}
else if (builder.Configuration.GetSection("AppSettings").GetSection("DataBaseType").Value == "SqlServer")
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServer");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

    // add distributed mysql server session cache
    builder.Services.AddDistributedSqlServerCache(options =>
    {
        options.ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
        options.SchemaName = builder.Configuration.GetSection("AppSettings").GetSection("DataBaseName").Value;
        options.TableName = "AppSessionCache";
        options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(60);
    });
    builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "SessionCookie";
            options.IdleTimeout = TimeSpan.FromMinutes(180);
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });
}

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.SignIn.RequireConfirmedEmail = true;
}).AddUserValidator<UserValidator>()
.AddPasswordValidator<PasswordValidator>()
.AddErrorDescriber<ErrorDescriber>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/User/Login");
    options.LogoutPath = new PathString("/User/Logout");
    options.AccessDeniedPath = new PathString("/Home/AccessDenied");

    options.Cookie = new()
    {
        Name = "IdentityCookie",
        HttpOnly = true,
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration.GetSection("AppSettings").GetSection("SessionCookieExpiration").Value));
});
builder.Services.AddAuthentication();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HrDepartmentPolicy", policy =>
    {
        policy.RequireClaim("Department", Enum.GetName(Department.HR)!);
    });

    options.AddPolicy("SoftwareDepartmentPolicy", policy =>
    {
        policy.RequireClaim("Department", Enum.GetName(Department.Software)!);
    });

    options.AddPolicy("EmployeePolicy", policy =>
    {
        policy.RequireClaim("Department", Enum.GetNames<Department>());
    });

    options.AddPolicy("FreeTrialPolicy", policy =>
    {
        policy.Requirements.Add(new FreeTrialExpireRequirement());
    });

    options.AddPolicy("AtLeast18Policy", policy =>
    {
        policy.AddRequirements(new MinimumAgeRequirement(18));
    });
});



var app = builder.Build();

// enable localization in request parameters
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


// use a custom Error Page for exceptions
app.UseExceptionHandler("/Errors/ErrorEx");
// use custom pages for Statuscodes;
app.UseStatusCodePagesWithReExecute("/Errors/ErrorCd/{0}");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

// if you want to log every request detail, enable this
//app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
