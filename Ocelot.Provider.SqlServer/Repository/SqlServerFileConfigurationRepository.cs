using Dapper;
using Newtonsoft.Json;
using Ocelot.Cache;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using Ocelot.Provider.SqlServer.Configuration;
using Ocelot.Provider.SqlServer.Extensions;
using Ocelot.Provider.SqlServer.Models;
using Ocelot.Responses;
using System.Data.SqlClient;

namespace Ocelot.Provider.SqlServer.Repository
{
    public class SqlServerFileConfigurationRepository : IFileConfigurationRepository
    {
        private readonly IOcelotCache<FileConfiguration> _cache;
        private readonly ConfigAuthLimitCacheOptions _option;

        public SqlServerFileConfigurationRepository(ConfigAuthLimitCacheOptions option,
            IOcelotCache<FileConfiguration> cache, IOcelotLoggerFactory loggerFactory)
        {
            _option = option;
            _cache = cache;
        }

        public Task<Response> Set(FileConfiguration fileConfiguration)
        {
            _cache.AddAndDelete(_option.CachePrefix + "FileConfiguration", fileConfiguration, TimeSpan.FromSeconds(1800), "");
            return Task.FromResult((Response)new OkResponse());
        }

        public async Task<Response<FileConfiguration>> Get()
        {
            var config = _cache.Get(_option.CachePrefix + "FileConfiguration", "");

            if (config != null)
            {
                return new OkResponse<FileConfiguration>(config);
            }

            var file = new FileConfiguration();
            const string sqlScript = "sp_OcelotGlobalConfiguration_GetFirst";

            await using (var connection = new SqlConnection(_option.DbConnectionStrings))
            {
                var result = await connection.QueryFirstOrDefaultAsync<OcelotGlobalConfiguration>(sqlScript);
                if (result != null)
                {
                    var glb = new FileGlobalConfiguration
                    {
                        BaseUrl = result.BaseUrl,
                        DownstreamScheme = result.DownstreamScheme,
                        RequestIdKey = result.RequestIdKey
                    };

                    if (!string.IsNullOrEmpty(result.HttpHandlerOptions))
                    {
                        glb.HttpHandlerOptions = result.HttpHandlerOptions.ToObject<FileHttpHandlerOptions>();
                    }
                    if (!string.IsNullOrEmpty(result.LoadBalancerOptions))
                    {
                        glb.LoadBalancerOptions = result.LoadBalancerOptions.ToObject<FileLoadBalancerOptions>();
                    }
                    if (!string.IsNullOrEmpty(result.QoSOptions))
                    {
                        glb.QoSOptions = result.QoSOptions.ToObject<FileQoSOptions>();
                    }
                    if (!string.IsNullOrEmpty(result.ServiceDiscoveryProvider))
                    {
                        glb.ServiceDiscoveryProvider = result.ServiceDiscoveryProvider.ToObject<FileServiceDiscoveryProvider>();
                    }
                    file.GlobalConfiguration = glb;

                    try
                    {
                        const string routeSql = "usp_OcelotRoutes_GetAll";

                        var ocelotRoutes = await connection.QueryAsync<OcelotRoutes>(routeSql,
                            new { OcelotGlobalConfigurationId = result.Id });

                        if (ocelotRoutes != null && ocelotRoutes.Any())
                        {
                            var routeList = new List<FileRoute>();
                            foreach (var model in ocelotRoutes)
                            {
                                var m = new FileRoute();
                                var fileRoute = JsonConvert.DeserializeObject<FileRoute>(model.Route);

                                if (fileRoute is null)
                                {
                                    continue;
                                }

                                if (fileRoute.AddHeadersToRequest != null)
                                {
                                    m.AddHeadersToRequest = fileRoute.AddHeadersToRequest;
                                }

                                if (fileRoute.AuthenticationOptions != null)
                                {
                                    m.AuthenticationOptions = fileRoute.AuthenticationOptions;
                                }

                                //if (!String.IsNullOrEmpty(fileRoute.CacheOptions))
                                //{
                                //    m.FileCacheOptions = model.CacheOptions.ToObject<FileCacheOptions>();
                                //}

                                if (fileRoute.DelegatingHandlers != null)
                                {
                                    m.DelegatingHandlers = fileRoute.DelegatingHandlers;
                                }

                                if (fileRoute.LoadBalancerOptions != null)
                                {
                                    m.LoadBalancerOptions = fileRoute.LoadBalancerOptions;
                                }

                                if (fileRoute.QoSOptions != null)
                                {
                                    m.QoSOptions = fileRoute.QoSOptions;
                                }

                                if (fileRoute.DownstreamHostAndPorts != null)
                                {
                                    m.DownstreamHostAndPorts = fileRoute.DownstreamHostAndPorts;
                                }

                                m.DownstreamPathTemplate = fileRoute.DownstreamPathTemplate;
                                m.DownstreamScheme = fileRoute.DownstreamScheme;
                                m.Key = fileRoute.Key;
                                m.Priority = fileRoute.Priority;
                                m.RequestIdKey = fileRoute.RequestIdKey;
                                m.ServiceName = fileRoute.ServiceName;
                                m.Timeout = fileRoute.Timeout;
                                m.UpstreamHost = fileRoute.UpstreamHost;

                                if (fileRoute.UpstreamHttpMethod != null)
                                {
                                    m.UpstreamHttpMethod = fileRoute.UpstreamHttpMethod;
                                }

                                m.UpstreamPathTemplate = fileRoute.UpstreamPathTemplate;
                                routeList.Add(m);
                            }
                            file.Routes = routeList;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
                else
                {
                    throw new Exception("Exception occurred in SqlServerFileConfigurationRepository");
                }
            }

            if (file.Routes == null || file.Routes.Count == 0)
            {
                return new OkResponse<FileConfiguration>(null);
            }
            return new OkResponse<FileConfiguration>(file);
        }
    }
}
