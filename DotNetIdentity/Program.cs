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
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;

[assembly: System.Reflection.AssemblyVersion("1.1.*")]

var builder = WebApplication.CreateBuilder(args);
var DbType = builder.Configuration.GetSection("AppSettings").GetSection("DataBaseType").Value;
var connectionString = string.Empty;

// database context
if (DbType == "MySql")
{
    connectionString = builder.Configuration.GetConnectionString("MySql");
    builder.Services.AddDbContext<AppDbContext, AppDbContextMySql>();    
}
else if (DbType == "SqlServer")
{
    connectionString = builder.Configuration.GetConnectionString("SqlServer");
    builder.Services.AddDbContext<AppDbContext, AppDbContextSqlServer>();
}
else if (DbType == "SqLite")
{
    connectionString = builder.Configuration.GetConnectionString("SqLite");
    builder.Services.AddDbContext<AppDbContext, AppDbContextSqLite>();
}

// add session
builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "SessionCookie";
            options.IdleTimeout = TimeSpan.FromMinutes(7);
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

// add serilog
var SeriLogConnStr = string.Empty;
if (DbType == "MySql")
{
    var MpropertiesToColumns = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    {
        ["Exception"] = "Exception",
        ["Level"] = "Level",
        ["Message"] = "Message",
        ["MessageTemplate"] = "MessageTemplate",
        ["Properties"] = "Properties",
        ["Timestamp"] = "Timestamp",
    };
    MariaDBSinkOptions mop = new MariaDBSinkOptions();
    mop.PropertiesToColumnsMapping = MpropertiesToColumns;
    mop.TimestampInUtc = false;
    mop.ExcludePropertiesWithDedicatedColumn = true;
    mop.EnumsAsInts = false;
    mop.LogRecordsCleanupFrequency = TimeSpan.FromDays(1);
    mop.LogRecordsExpiration = TimeSpan.FromDays(30);
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        // Filter out ASP.NET Core infrastructre logs that are Information and below
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        // Filter out ASP.NET EntityFramework logs that are Information and below
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        //.ReadFrom.Configuration(builder.Configuration)    
        .WriteTo.MariaDB(
            connectionString: connectionString,
            tableName: "AppLogs",
            autoCreateTable: true,
            useBulkInsert: false,
            options: mop
        )
        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
        .CreateLogger();
}
else if (DbType == "SqlServer")
{
    var sinkOpts = new MSSqlServerSinkOptions
    {
        TableName = "AppLogs",
        AutoCreateSqlTable = true,
    };
    var columnOpts = new ColumnOptions();
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        // Filter out ASP.NET Core infrastructre logs that are Information and below
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        // Filter out ASP.NET EntityFramework logs that are Information and below
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(
            connectionString: connectionString,
            sinkOptions: sinkOpts
        )
        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
        .CreateLogger();
}
else if (DbType == "SqLite")
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        // Filter out ASP.NET Core infrastructre logs that are Information and below
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        // Filter out ASP.NET EntityFramework logs that are Information and below
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
        .WriteTo.SQLite(sqliteDbPath: Environment.CurrentDirectory + "/" + builder.Configuration.GetConnectionString("SqLite")!.Replace("Data Source=",""), tableName: "AppLogsSqLite", batchSize: 1)
        .CreateLogger();
}
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

// add own services
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
builder.Services.AddScoped<IAuthorizationHandler, FreeTrialExpireHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddTransient<UserManager<AppUser>>();
builder.Services.AddScoped<EmailHelper>();
builder.Services.AddScoped<TwoFactorAuthenticationService>();

// add AspNetCore.Identity options
builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;

        options.Password.RequireDigit = false;
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

// set cookie options
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

// add authentication & authorization
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

if (DbType != "SqlServer" && DbType != "MySql" && DbType != "SqLite")
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] " + DbType + " is an invalid Database type. Please take a look into your appsettings.json!");
    Console.ForegroundColor = ConsoleColor.White;
    await app.StopAsync();
}

// migrate initial
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] Database type is: " + DbType);
Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] check if initial migration is enabled....");
Console.ForegroundColor = ConsoleColor.White;

if(builder.Configuration.GetSection("AppSettings").GetSection("MigrateOnStartup").Value=="true") {
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] initial migration is enabled! Try to apply migration for Database type: " + DbType);
    Console.ForegroundColor = ConsoleColor.White;
    using (var scope = app.Services.CreateScope())
    {
        if (DbType == "MySql")
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dataContext.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync("Initial_MySql");
        }
        else if (DbType == "SqlServer")
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dataContext.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync("Initial_SqlServer");
        }
        else if (DbType == "SqLite")
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dataContext.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync("Initial_SqLite");
        }
    }
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] initial migration migration successfully applied for Database type: " + DbType);
    Console.ForegroundColor = ConsoleColor.White;
} else {
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + " INF] initial migration is disabled! Skipping intial migration.");
    Console.ForegroundColor = ConsoleColor.White;
}
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
app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

