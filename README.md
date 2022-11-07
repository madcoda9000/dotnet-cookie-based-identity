# .NET 6 Identity Cookie Based Authentication

## What's this?

Everyone knows, if you start a new project you've to implement core things like: Authentication, Authorization, User management, Roles management, Logging, Errorhandling etc...

I was tired of doing so and created this boilerplate. I was isnpired by this Article [ASP.NET Core: From Beginner to advanced](https://burakneis.com/asp-net-core-identity/)

This boilerplate provides the following features:

- using .NET Core 6
- using entity Framework
- supports MySql, MariaDb and SqlServer
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
- provides doxygen generated class documentation\\
  - take a look [here](https://htmlpreview.github.io/?https://github.com/madcoda9000/dotnet-cookie-based-identity/blob/main/Documentation/generated/html/index.html)

### Huhhh, sounds interesting! Can i take a look at it?

sure, I've prepared a few [Screenshots](Documentation/SCREENSHOTS.md) for you!

### Yeah, looks nice. Is the source code documented too?

sure it is. Take a look [here](Documentation/generated/latex/refman.pdf)

### Damn! You've coded that all by yourself?

No! I've used the following Librarys / Projects in this Project:

- Parts of this Article [ASP.NET Core: From Beginner to advanced](https://burakneis.com/asp-net-core-identity/)
- [Identity Server 4](https://github.com/IdentityServer/IdentityServer4) for ASP.NET Core Identity management. I've used this version because the actual version is not free anymore! Duende decided to force a fee for commercial use of Identity Server 5.
  - License: [Apache License 2.0](https://github.com/IdentityServer/IdentityServer4/blob/main/LICENSE)
- [DatatablesJs](https://github.com/ekondur/DatatableJS) (I've modified that and included the modified source [here](DatatablesJs). The changes I've made are documented [here](https://github.com/ekondur/DatatableJS/issues)
  - License: [MIT](https://github.com/ekondur/DatatableJS/blob/main/LICENSE.md)
- [Datatables Js](https://datatables.net/) (the client library!).
  - License: [MIT](https://datatables.net/license/mit)
- [DoxyGen](Https://doxygen.nl) for generating the class documantation, which can be found [here](Documentation/generated/html/)
  - License: [GPL 2.0](https://github.com/doxygen/doxygen/blob/master/LICENSE)
- [Bootstrap 5.1](https://getbootstrap.com) for creating the layouts
  - License: [MIT](https://github.com/twbs/bootstrap/blob/v4.0.0/LICENSE)
- [Jquery](https://jquery.com) for making javascript easier :-)
  - License: [MIT](https://jquery.org/license/)
- [Toastify](https://apvarun.github.io/toastify-js/) for creating nice toast messages
  - License: [MIT](https://github.com/apvarun/toastify-js/blob/master/LICENSE)
- [Bootstrap Session Timeout](https://jquery-plugins.net/bootstrap-session-timeout) for detecting activity and do a session timeout
  - License: [MIT](https://github.com/orangehill/bootstrap-session-timeout/blob/master/LICENSE.md)

### But, these are using all different licenses! Can i use that for a commercial project anyway?

at first, you've to respect the license of the projects I've listetd here! That's why i listed the projects here with the corosponding link.  
As i am writing this documentation they provide a free (for commercial too) License! **Anyway, they can change their license. So, before using this in an commercial project, do yourself a favour and visit their page to verify that they provide a free license that is suitable for commercial use!**

The code I've written is provided using the MIT License. See [here](LICENSE.md)

### Ahhh, that's exactly what i need. How can i use that?

so, then take a look at the install [instrunctions](Documentation/INSTALLmd)

### Is there a changelog?

Sure, take a look [here](CHANGELOG.md)
