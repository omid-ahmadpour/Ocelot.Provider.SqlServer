using System.ComponentModel.DataAnnotations;

namespace Ocelot.Provider.SqlServer.Models
{
    public class OcelotGlobalConfiguration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string GatewayName { get; set; }

        [StringLength(100)]
        public string? RequestIdKey { get; set; }

        [StringLength(100)]
        public string? BaseUrl { get; set; }

        [StringLength(50)]
        public string? DownstreamScheme { get; set; }

        [StringLength(300)]
        public string? ServiceDiscoveryProvider { get; set; }

        [StringLength(300)]
        public string? QoSOptions { get; set; }

        [StringLength(300)]
        public string? LoadBalancerOptions { get; set; }

        [StringLength(300)]
        public string? HttpHandlerOptions { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
