 ![image](Screenshots/netBanner.png)

# [.NET 8 Identity Cookie based](https://github.com/madcoda9000/dotnet-cookie-based-identity) - Changelog

All notable changes to this project will be documented in this file.

## \[Release v.1.1.0\]

### \[Build: 1.1.9073.11202\] - 2024-11-03

* added seceral fixes to ensure web standards are fullfilled
* apllied performace fixes
* tested stndard compliance and page speed with Lighthouse
* updated Documentation


### \[Build: 1.1.9072.35247\] - 2024.11.02

* Updated complete project to .NET Core 8.0 LTS


### \[Build: 1.0.9072.34217\] - 2024.11.02

### Modified

* Removed DataTablesJs dependencies
* Removed DataTablesJs folder
* Removed DataTables objects and methods from AdminController.cs
* Removed AuditLogs.cshtml
* Removed SystemLogs.cshtml
* Removed ErrorLOgs.cshtml
* Added LogsError.cshtml
* Added LogsSystem.cshtml
* Added LogsAudit.cshtml
* Updated Documentation

## \[Release v.1.0.0\]

### \[1.0.8357.29989\] - 2022.11.18

#### Added

* added translation for Style groups in SettingsBrand.cshtml
* added audit log entry on logout due to session timeout
* added gender Unknown on initial seed for user super.admin

#### Fixed

* displaying max image dimension for logon background in SettingsBrand.cshtml

### \[1.0.8357.22226\] - 2022.11.18

#### Added

* added property ShowMfaWarningBanner<bool> to Models/Businessmodels/GlobalSettings.cs
* added property seed for ShowMfaWarningBanner to AppDbContextMySql
* added property seed for ShowMfaWarningBanner to AppDbContextSqlServer
* added property seed for ShowMfaWarningBanner to AppDbContextSqLite
* added checkbox for ShowMfaWarningBanner to SettingsGlobal view
* added translations for SettingsGlobal view
* added jquery 2fa disabled warning based on global settings to _MainLayout.cshtml
* added translations for 2fa warning banner in _MainLayout.de.resx
* added translations for 2fa warning banner in _MainLayout.de.resx
* generated new Migrations for SqlServer context
* generated new Migration for MySql context
* generated new MIgration for SqLite context
* updated SettingsApp POST method in SettingsController to save new property

#### Fixed

* wrong title color in Sessiontimeout dialog

## \[Unreleased\]

### \[0.0.8357.12450\] - 2022.11.18

#### Added

* added new initial migrations
* added favicon.ico
* added images for documentation

#### Modified

* changed images in SCREENSHOTS.md
* updated INSTALL.md

#### Removed

* removed distributed session cache

### \[0.0.8356.26140\] - 2022.11.17

#### Added

* added MigrationOnStartup parameter in appsettings.json
* added condition to check if MigrationOnStartup is true in program.cs
* added INSTALL.md

#### Modified

* changed Home/index content 
* added PhoneNumber property to inital seed in dbContext
* created new initial migrations for SqLite
* created new initial migrations for MySql
* created new initial migrations for MS-Sql

### \[0.0.8355.23480\] - 2022.11.16

#### Added

* added sqlite database contex
* added sqlite condition for serilog in program.cs
* added sqlite condition for intitial migration
* added sqlite initial migration
* added sqlite database connectionstring to appsettings.json
* added sqlite database connectionstring to appsettings.TEMPLATE.json
* added additional AppLogsSql.cs class to capture sqlite serilog sink table differences

#### Modified

* modified AdminController to reflect Serilog table differences using sqlite sink
* added *.sqlite to gitignore


#### Fixed

* naming error in AdminController for Applicationsettings

### \[0.0.8355.18854\] - 2022.11.16

#### Added

* added global AppDbContext.cs
* added MS-SqlServer dbContext: AppDbContextSqlServer.cs
* added MySql-Server dbContext: AppDbContextMySql.cs
* added Intitial Migrations for SqlServer
* added Initial MIgrations for MySql
* added automatic intitial migration depending on db type in program.cs

#### Modified

* AdminController.cs modified Log actions to reflect new AppLogs Schema
* ErrorLogs.cshtml to reflect new AppLogs Schema
* AuditLogs.cshtml to reflect new AppLogs Schema
* SystemLogs.shtml to reflect new AppLogs Schema
* changed property EnumsAsInt to false for SeriLog configuration
* changed DbContext Registration in program.cs
* removed unneccessary comments from SettingsService.cs

### \[0.0.8354.20556\] - 2022.11.15

#### Added

* added condition to check if mysql or sqlserver is used for logger configuration in program.cs
* added mysql SeriLog configuration to program.cs
* added SqlServer SeriLog configuration to program.cs
* added database type conditional automatic migration in program.cs
* added DataBaseName extraction from connectionstring to set Schema name for the distributed Session cache


### Modified

* removed SeriLog Settings from appsettings.json
* removed creating / deleting AppLogs table from intial migration as the table is genrated automatically by SeriLog
* grouped Brand Settings in SettingsBrand.cshtml
* changed Timestamp property in AppLogs.cs from string? to DateTime
* removed SeriLogs Settings from appsettings.TEMPLATE.json
* removed DataBaseName property from appsettings
* removed DataBaseName property from appsettings.TEMPLATE.json

### \[0.0.8353.16277\] - 2022.11.14

#### Fixed

* image display on auth layout
* css background theme color for inputs in AuthLayout

### \[0.0.8352.10688\] - 2022.11.13

#### Added

* added link color attribute to brand settings
* added Headlines aatribute to brand settings
* added muted text attribute to brand settings
* added translations for new properties
* added Auth pages Backround property to brand settings
* added dynamic background to _AuthLayout.cshtml

#### Modified

* modified settings controller POST method to save custom images
* replaced css values with brand settings property in _MainLayout.cshtml
* replaced css values with brand settings property in _AuthLayout.cshtml
* replaced css values with brand settings property in _ErrorLayout.cshtml
* replaced static logo on login.cshtml with brand settings property

### \[0.0.8351.15971\] - 2022.11.12

#### Added

* restore Brand settings to SettingsBrand view
* restore Brand settings action to Settings controller

### \[0.0.8351.14425\] - 2022.11.12

#### Added

* added class BrandSettings.cs
* added seeding values for BrandSettings to AppDbContext.cs
* added property Brand type of BrandSettings to ISettingsService
* extend SettingsService with Brandsettings
* added ApplicationName property to BrendSettings class
* added Controller action for SettingsBrand view
* added controller POST method to update BrandSettings from SettingsBrand view
* added view SettingsBrand.cshtml
* added SettingsBrand to _NavMenu.cshtml
* added Logo image property to BrandSettings class

#### Modified
* deleted ApplicationName property from GlobalSettings.cs
* changed Seed command for ApplicationName in AppDbContext
* used new properties of BrandSettings in style Tag of _MainLayout
* used new properties of BrandSettings in style Tag of _AuthLayout
* implemented dynamic Application Logo in SettingsBrand view
* updated README.md

### \[0.0.8350.17091\] - 2022.11.11

#### Added

* added global.json to bind to .Net core 6.0.403
* check if current user is a ldap user in _NavMenu.cshtml. If so, hide change password menu entry.
* check if current user is a ldap user in _MainLayout.cshtml. If so, hide change password menu entry.
* added release & build version to _MainLayout.cshtml

#### Modified

* disabled check if username is part of the email address
* modified Login POST method in UserController.cs to set password on ldap login
* added IsLdapUser Claim in ClaimTransformation Class

#### Fixed

* wrong label translation in SettingsLdap.cshtml
* wrong property assignment for domain controller
* possible null reference in EditUser.cshtml

### \[0.0.8348.12196\] - 2022.11.09

#### Added

* new Parameter in appsettings.json for DataBaseName in Section AppSetting
* LDAP Authentication to UserController.cs
* Added IsLdap Property to EditUserViewModel.cs
* added IsLdap Property to EditUserView
* added IsLdap Property to NewUser view

#### Modified

* changed setting Schema name from static string to read from appsettings.json
* modified EditUser Post method in AdminController.cs to reflect LDAP property
* modified ActionResult EditUser in AdminController.cs to reflect LDAP property

#### Fixed

* fixed Error: Table AppSessionCache does not exist

### \[0.0.8347.13229\] - 2022-12-07

#### Added

* added Translation for SettingsApp.cshtml
* added Translation for SettingsMail.cshtml
* added Translation for SettingsLdap.cshtml
* added Translation for AdminController.cs
* added Translation for SettingsController.cs

### \[0.0.8346.16799\] - 2022-11-07

#### Added

* versionnumber in _Authlayout.cshtml
* Settings controller
* SettingsLdap view
* SettingsMail view
* SettingsApp view
* added CHANGELOG.md

#### Modified

* README.md: added changelog link
* extend AppUser class with RolesCombined property
* extend AdminController NewUser method to make use of new property
* extend AdminController EditUser method to make use of new property
* extend view Admin/Users to make use of new property

#### Fixed

* Nullable variable in _AuthLayout.cshtml

### \[0.0.8346.16425\] - 2022-11-06

#### Published

* source published on Github


