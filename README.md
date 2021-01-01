# Blazor Repository Pattern
Usage of the Unit Of Work and repository pattern for a Blazor server project.

### Projects Structure

```
+-+-(src)--+- WordsManager: Blazor server-side web application
  |        |
  |        +- Wwg.Core: Business/Application Model
  |        |
  |        +- Wwg.DirectoryService: External Dictionary Service Wrapper
  |        |
  |        +- Wwg.Infrastructure: Data Access Logic
  |        |
  |        +- Wwg.Services: Data Access Logic
  |
  +-(test)-+- Wwg.Core.Test: Wwg.Core Unit Test
           |
           +- Wwg.DirectoryService.Test: Wwg.DirectoryService Unit Test
```

### Build Environment

- [Visual Studio 2019 v16.8.2](https://visualstudio.microsoft.com/vs/)
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [EgBlazorComponents](https://www.nuget.org/packages/EgBlazorComponents)

### Database
I chose [SQLite](#references) for this project as it is lightweight component and can be easily embedded into a project. 

#### Create Database
This source code does not include the database file (*.db).
It can be done by the following steps:

1. Select the Wwg.Infrastructure as the default project.
2. Use the command lines below. The argument for Update-Database is the location where the *.db file will be created into.

```
Add-Migration InitialCreate -Args '--path "D:\SQLite Files\words.db"'
Update-Database -Args '--path "D:\SQLite Files\words.db"'
```

## Copyright
I do not own the copyright to any dictionary service and content.
You need to contact the service provider to use their service to extend the sample project or make your own version.

#### References
* [Common web application architectures](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
* [Design-time DbContext Creation](https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation)
* [Getting Started with EF Core](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app)
* [Microsoft eShopOnWeb ASP.NET Core Reference Application](https://github.com/dotnet-architecture/eShopOnWeb)
* [An Overview of eShopOnWeb, an ASP.NET Core Reference Application](https://youtu.be/vRZ8ucGac8M)
* [SQLite Official](https://www.sqlite.org/)
* [Google Dictionary API](https://github.com/meetDeveloper/googleDictionaryAPI)