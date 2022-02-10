# Ocelot.Provider.SqlServer

## Getting Started
Ocelot.Provider.SqlServer is designed to work with ASP.NET and is currently on net6.0.

With this package you can save the routes in sql server instead of Json file and add the route dynamically.

## .NET 6.0

### 1. Install NuGet package
Install Ocelot.Provider.SqlServer and it’s dependencies using nuget. You will need to create a net6.0 project and bring the package into it. Then follow the Startup below and Configuration sections to get up and running.

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

## 4. Add SqlServer connection string in appsettings.json.
```
"ConnectionStrings": {
    "SqlServerDb": "Server=(localdb)\\mssqllocaldb;Database=OcelotDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
 ```
