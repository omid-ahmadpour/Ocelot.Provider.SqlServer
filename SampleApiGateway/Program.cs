using Ocelot.DependencyInjection;
using Ocelot.Provider.SqlServer.DependencyInjection;
using Ocelot.Provider.SqlServer.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot()
    .AddSqlServerProvider(options =>
    {
        options.DbConnectionStrings = builder.Configuration.GetConnectionString("SqlServerDb");
        options.MigrationsAssembly = Assembly.GetExecutingAssembly().FullName;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOcelotWithSqlServerProvider().Wait();

app.Run();
