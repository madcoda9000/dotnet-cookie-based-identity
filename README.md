# .NET 6 Identity Cookie Based Authentication

## What's this?

Everyone knows, if you start a new project you've to implement core things like: Authentication, Authorization, User management, Roles management, Logging, Errorhandling etc...

I was tired of doing so and created this boilerplate. I was inspired by this Article [ASP.NET Core Identity: From Beginner to advanced](https://burakneis.com/asp-net-core-identity/)

This boilerplate provides the following features:

* using .NET Core 6
* using entity Framework
* supports MySql, MariaDb and SqlServer
* Provides Authentication with Identity Framework and LDAP\
  Login\
  Register\
  Forgot Password\
  Change Password\
  optional email verification\
  2fa by Authenticator App (Microsoft or Google)\
  2fa by email
* Provides Authorization\
  Claim based\
  role based
* provides global error handling\
  for status errors\
  for exceptions
* provides application settings service
  easy to extend settings service with own settings classes\
* provides session cache in database\
* provides loggin using serilog\
  audit logging\
  exception logging\
  application logging
* provides usermanagement
  create, edit users\
  enforce 2fa\
  set ldap login\
  assign roles\
  enable / disable users
* provides role management\
  create / edit roles
* provides user selfservice\
  reset password\
  change password\
  edit profile\
  edit profile picture (stored in database)
* provides a bootstrap based theme\
  only three layout files (Main, Auth, Error)\
  easy to customize
* provides doxygen generated class documentation\
  take a look [here](https://htmlpreview.github.io/?https://github.com/madcoda9000/dotnet-cookie-based-identity/blob/main/Documentation/generated/html/index.html)

### Ahhh, that's exactly what i need. How can i use that?

so, than take a look at the install [instructions](https://github.com/madcoda9000/dotnet-cookie-based-identity/blob/main/Documentation/Install.md).
