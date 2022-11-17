# Installation instructions

## Requirements

* install Microsoft .Net Core 6.0 SDK [(donload here)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
  * ensure .Net Framweork is installed by opening a Powershell or terminal window.
  * type: `dotnet --info`
    If you get a list with installed SDK's, everything is fine.
* install Dotnet EntityFramework Core tools
  * open a powershell window (or a terminal on Linux or MacOs)
  * type: `dotnet tool install --global dotnet-ef`
  * verify successful installation by typing: `dotnet ef`
* if you want to use a Database like MySql or MS-SqlServer, ensure that they are up and accessible. This ist not neccessary as this apllication can use SqLite too.
  But for a production enviroment i strongly recommend to use a seperate database server.
* **OPTIONAL**: install a development enviroment of your choice (for ex.: VsCode, Rider etc..)

## Installation

### Preparations

* download the latest [release](https://github.com/madcoda9000/dotnet-cookie-based-identity/releases)
* extract the zip to a location of your choice
* open a Powershell or Terminal (on Mac or Linux) and navigate into the extraced folder
  **TIPP:** if you're using a development enviroment you can open that extracted folder with your developemnt enviroment and use the terminal builtin.
  **NOTE**: ensure you're in the folder that contains the **DotNetIdentity.csproj** file!
* rename appsettings.TEMPLATE.jso to appsettings.json
  Mac / Linux Terminal: `mv appsettings.TEMPLATE.json appsettings.json`
  Windows Powershell: `Rename-Item .\appsettings.TEMPLATE.json -NewName appsettings.json`
* modify the appropiate connection string (MySql, SqlServer or SqLite) in appsettings.json to match your enviroment
* modify the value of the Setting **DataBaseType** under **AppSettings** in appsettings.json to match your connectionstring
* set the value of the Setting **MigrateOnStartup** under **AppSettings** in appsettings.json to **true** (if not already)

### the magic.. (if everthing goes well!)

* type dotnet run
  The application will try to compile and start. Whe you've no typo in you connectionstring and your Database server is reachable, the application will try to create the database a  nd the tables. After that the application will start a browser and open the application.
  Your console window output should look like that:
  ![dotnet-run1](Screenshots/dotnet-run1.png)
  **HINT**: if the browser is not launching automatically, open a browser manually an enter this url: https://localhost:7106

* stop the application by pressing the following in the terminal (or Powershell) window where you've started the application
  Mac: CMD + C
  Linux & Windows: STRG + C
  The application will shutdown now.
* set the value of the Setting **MigrateOnStartup** under **AppSettings** in appsettings.json to **false**
  Otherwise the application will check and try to migrate the initial Database setup on every launch. That is not a problem in a development enviroment. But I would reccomend to disable this.
* now you can start the application by typing `dotnet run`
  Your console window output should look like that:
  ![dotnet-run2](Screenshots/dotnet-run2.png)

* You can login using the following credentials:
  **Username**: super.admin
  **Password**: Test1000!

