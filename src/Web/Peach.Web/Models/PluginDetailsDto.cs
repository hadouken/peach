using Peach.Data.Domain;

namespace Peach.Web.Models
{
    public class PluginDetailsDto
    {
        public bool ShowEditLink { get; set; }

        public Plugin Plugin { get; set; }
    }
}