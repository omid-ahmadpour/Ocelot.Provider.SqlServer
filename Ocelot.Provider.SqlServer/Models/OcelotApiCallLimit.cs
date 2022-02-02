using System.ComponentModel.DataAnnotations;

namespace Ocelot.Provider.SqlServer.Models
{
    public class OcelotApiCallLimit
    {
        [Key]
        public long Id { get; set; }

        public string ClientId { get; set; }

        public byte DurationTypeId { get; set; }

        public int CallCount { get; set; }

        public string UpstreamPath { get; set; }
    }
}
