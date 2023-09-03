# Ocelot.Provider.SqlServer

Welcome to Ocelot.Provider.SqlServer, the SQL Server provider for Ocelot - your gateway to dynamic routing in ASP.NET applications. With Ocelot.Provider.SqlServer, you can effortlessly store your Ocelot routes in a SQL Server database instead of a JSON file, enabling dynamic and scalable routing configurations.

## Getting Started with .NET 6.0

Follow these steps to integrate Ocelot.Provider.SqlServer into your .NET 6.0 project:

### 1. Install the NuGet Package

Begin by installing the `Ocelot.Provider.SqlServer` NuGet package into your .NET 6.0 project using your preferred package manager:

```bash
> Install-Package Ocelot.Provider.SqlServer
```

[![NuGet Package](https://img.shields.io/nuget/v/Ocelot.Provider.SqlServer.svg)](https://www.nuget.org/packages/Ocelot.Provider.SqlServer/)

### 2. Register Ocelot.Provider.SqlServer in `Program.cs`

In your `Program.cs` file, configure Ocelot.Provider.SqlServer by adding it to the services collection:

```csharp
builder.Services.AddOcelot()
    .AddSqlServerProvider(options =>
    {
        options.DbConnectionStrings = builder.Configuration.GetConnectionString("SqlServerDb");
        options.MigrationsAssembly = Assembly.GetExecutingAssembly().FullName;
    });
```

### 3. Add Middleware

In your `Startup.cs`, use the Ocelot.Provider.SqlServer middleware to enable dynamic routing:

```csharp
app.UseOcelotWithSqlServerProvider().Wait();
```

### 4. Configure SQL Server Connection String

In your `appsettings.json`, define your SQL Server connection string as follows:

```json
"ConnectionStrings": {
    "SqlServerDb": "Server=(localdb)\\mssqllocaldb;Database=OcelotDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 5. Database Migration

To set up the required database tables, follow these steps:

#### First:

Set your API Gateway project as the startup project in Visual Studio.

#### Second:

Run the following Entity Framework Core commands in the Package Manager Console:

##### Add Migration

```bash
> Add-Migration InitDb
```

##### Update Database

```bash
> Update-Database
```

### Check Your Database

After completing the migration, your database will contain two tables:

- `OcelotRoutes`: Add your routes to this table.
- `OcelotGlobalConfigurations`

## Sample Projects

Explore sample projects using Ocelot.Provider.SqlServer for both the API Gateway and APIs:

- [Sample for API Gateway](https://github.com/omid-ahmadpour/Ocelot.Provider.SqlServer/tree/master/SampleApiGateway)
- [Sample for API](https://github.com/omid-ahmadpour/Ocelot.Provider.SqlServer/tree/master/SampleApi)

## Share Your Feedback

We appreciate your feedback, suggestions, and contributions. Please feel free to open issues or reach out with your thoughts. We respond to every issue or message and are eager to improve this package. Thank you for using Ocelot.Provider.SqlServer!
