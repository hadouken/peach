using System;

namespace Peach.WebApi.Models
{
    public class PluginListItem
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public Uri Homepage { get; set; }

        public string Description { get; set; }

        public ReleaseItem LatestRelease { get; set; }
    }
}