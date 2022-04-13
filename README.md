# Ocelot.Provider.SqlServer

## Getting Started
Ocelot.Provider.SqlServer is designed to work with ASP.NET and is currently on net6.0.

With this package you can store the Ocelot routes in sql server instead of Json file and add route dynamically.

## .NET 6.0

### 1. Install NuGet package
Install Ocelot.Provider.SqlServer using nuget. You will need to create a net6.0 project and bring the package into it. Then follow the Startup below and Configuration sections to get up and running.

```ruby
  > Install-Package Ocelot.Provicer.SqlServer
  ```
  
## 2. Register Ocelot.Provider.SqlServer
### in Program.cs

```ruby
> builder.Services.AddOcelot()
    .AddSqlServerProvider(options =>
    {
        options.DbConnectionStrings = builder.Configuration.GetConnectionString("SqlServerDb");
        options.MigrationsAssembly = Assembly.GetExecutingAssembly().FullName;
    });
```

## 3. Add Middleware

```ruby
> app.UseOcelotWithSqlServerProvider().Wait();
```

## 4. Add SqlServer connection string in appsettings.json
```
"ConnectionStrings": {
    "SqlServerDb": "Server=(localdb)\\mssqllocaldb;Database=OcelotDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
 ```
 
 ## 5. Database Migration
 
  ### First:
  Set your apigateway project as startup project
  
  ### Second:
  Run following ef command in Package Manager Console(with visual studio)
  #### add migration
  ```ruby
  > Add-Migration InitDb
  ```
  #### update database
  ```ruby
  > Update-Database
  ```
  
  ### Now check your database, you have two tables:
  #### OcelotRoutes --> add your route to this table
  #### OcelotGlobalConfigurations
  
  ### The source of a project that used Ocelot.Provider.SqlServer is also included.
> [Sample for ApiGateway](https://github.com/omid-ahmadpour/Ocelot.Provider.SqlServer/tree/master/SampleApiGateway)
> 
> [Sample for Api](https://github.com/omid-ahmadpour/Ocelot.Provider.SqlServer/tree/master/SampleApi)

##
Please share your thoughts, feedback or suggestions.
I would love to hear from you and I response to every single issue or message.
