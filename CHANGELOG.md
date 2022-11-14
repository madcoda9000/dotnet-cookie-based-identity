# .NET 6 Identity Cookie Based Authentication Changelog

All notable changes to this project will be documented in this file.

## \[Unreleased\]

### \[0.0.8352.10688\] - 2022.11.13

#### Fixed

* image display on auth layout
* css background theme color for inputs in AuthLayout

### \[0.0.8352.10688\] - 2022.11.12

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


