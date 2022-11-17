![image](Documentation/Screenshots/netBanner.png)

## What's this?

Everyone knows, if you start a new project you've to implement core things like: Authentication, Authorization, User management, Roles management, Logging, Errorhandling etc...

I was tired of doing so and created this boilerplate. I was inspired by this Article [ASP.NET Core: From Beginner to advanced](https://burakneis.com/asp-net-core-identity/)

* [Changelog](Documentation/CHANGELOG.md)
* [Credits](Documentation/CREDITS.md)
* [License](Documentation/LICENSE.md)
* [Screenshots](Documentation/SCREENSHOTS.md)

### Features

- using .NET Core 6
- using entity Framework
- supports MySql, MariaDb, SqLite and SqlServer
- Provides Authentication with Identity Framework and LDAP
  - Login
  - Register
  - Forgot Password
  - Change Password
  - optional email verification
  - 2fa by Authenticator App (Microsoft or Google)
  - 2fa by email
- Provides Authorization
  - Claim based
  - role based
- provides global error handling
  - for status errors
  - for exceptions
- provides application settings service
  - easy to extend settings service with own settings classes
- provides session cache in database
- provides session timeout due to inactivity
- provides loggin using serilog
  - audit logging
  - exception logging
  - application logging
- provides usermanagement
  - create, edit users
  - enforce 2fa
  - set ldap login
  - assign roles
  - enable / disable users
- provides role management
  - create / edit roles
- provides user selfservice
  - reset password
  - change password
  - edit profile
  - edit profile picture (stored in database)
- is multilanguage
  - implemented english translation
  - implemented german translation
  - additional languages can easily implemented with addidtional ressource files
- provides a bootstrap based theme
  - only three layout files (Main, Auth, Error)
  - easy to customize
- provides easy Brand customizing
  - set Application name
  - set primary color
  - set warning color
  - set info color
  - set success color
  - set danger color
  - set background color
  - set Application Logo
  - Restore Brand settings to default

