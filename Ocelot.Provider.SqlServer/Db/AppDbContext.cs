using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Ocelot.Provider.SqlServer.Configuration;
using Ocelot.Provider.SqlServer.Models;

namespace Ocelot.Provider.SqlServer.Db
{
    public class AppDbContext : DbContext
    {
        private readonly AppConfigs _appConfigs;
        private readonly string _jsonRouteSample = @"{
        'DownstreamPathTemplate': '/{everything}',
        'DownstreamScheme': 'http',
        'DownstreamHostAndPorts': [
            {
                'Host': 'localhost',
                'Port': 5095
            }
        ],
        'UpstreamPathTemplate': '/gateway/{everything}',
        'UpstreamHttpMethod': [
            'Get'
        ]}";

        public AppDbContext(IOptions<AppConfigs> options)
        {
            _appConfigs = options.Value;
        }

        public DbSet<OcelotGlobalConfiguration> OcelotGlobalConfigurations { get; set; }

        public DbSet<OcelotRoute> OcelotRoutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appConfigs.DbConnectionStrings,
                b => b.MigrationsAssembly(_appConfigs.MigrationsAssembly));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OcelotGlobalConfiguration>().HasData(new OcelotGlobalConfiguration { Id = 1, GatewayName = "TestGateway" });

            var json = JObject.Parse(_jsonRouteSample);
            modelBuilder.Entity<OcelotRoute>().HasData(new OcelotRoute { Id = 1, Route = json.ToString() });
        }
    }
}
