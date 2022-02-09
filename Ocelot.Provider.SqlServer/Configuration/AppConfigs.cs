namespace Ocelot.Provider.SqlServer.Configuration
{
    public class AppConfigs
    {
        /// <summary>
        /// true
        /// </summary>
        public bool EnableRateLimit { get; set; } = true;

        /// <summary>
        /// true
        /// </summary>
        public bool EnableAuthorization { get; set; } = true;

        /// <summary>
        /// client_id
        /// </summary>
        public string ClientKey { get; set; } = "client_id";

        public int CacheExpireTime { get; set; } = 3600;

        public string CachePrefix { get; set; } = "oce";

        /// <summary>
        /// SQLSERVER
        /// </summary>
        public string DbConnectionStrings { get; set; }

        /// <summary>
        /// Redis
        /// </summary>
        public List<string> RedisConnectionStrings { get; set; }
    }
}
