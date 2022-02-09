using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ocelot.Provider.SqlServer.Configuration;
using Ocelot.Provider.SqlServer.Models;

namespace Ocelot.Provider.SqlServer.Db
{
    public class AppDbContext : DbContext
    {
        private readonly AppConfigs _appConfigs;

        public AppDbContext(IOptions<AppConfigs> options)
        {
            _appConfigs = options.Value;
        }

        public DbSet<OcelotGlobalConfiguration> OcelotGlobalConfigurations { get; set; }

        public DbSet<OcelotRoute> OcelotRoutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appConfigs.DbConnectionStrings);
        }
    }
}
