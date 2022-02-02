using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Provider.SqlServer.Configuration;
using Ocelot.Provider.SqlServer.Repository;

namespace Ocelot.Provider.SqlServer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IOcelotBuilder AddSqlServerForRoutesStorage(this IOcelotBuilder builder, Action<ConfigAuthLimitCacheOptions> option)
        {
            builder.Services.Configure(option);

            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<ConfigAuthLimitCacheOptions>>().Value);

            builder.Services.AddSingleton<IFileConfigurationRepository, SqlServerFileConfigurationRepository>();

            return builder;
        }
    }
}
