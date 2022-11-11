# .NET 6 Identity Cookie Based Authentication Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### [0.0.8350.17091] - 2022.11.09

#### Added

* added global.json to bind to .Net core 6.0.403
* check if current user is a ldap user in _NavMenu.cshtml. If so, hide change password menu entry.
* check if current user is a ldap user in _MainLayout.cshtml. If so, hide change password menu entry.

#### Modified

* disabled check if username is part of the email address
* modified Login POST methos in UserController.cs to set password on ldap login
* added IsLdapUser Claim in ClaimTransformation Class

#### Fixed

* wrong label translation in SettingsLdap.cshtml
* wrong property assignment for domain controller

### [0.0.8348.12196] - 2022.11.09

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

### [0.0.8347.13229] - 2022-12-07

#### Added

* added Translation for SettingsApp.cshtml
* added Translation for SettingsMail.cshtml
* added Translation for SettingsLdap.cshtml
* added Translation for AdminController.cs
* added Translation for SettingsController.cs

### [0.0.8346.16799] - 2022-11-07

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

### [0.0.8346.16425] - 2022-11-06

#### Published

* source published on Github


