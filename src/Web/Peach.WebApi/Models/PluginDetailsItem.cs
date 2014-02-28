using System;
using System.Collections.Generic;

namespace Peach.WebApi.Models
{
    public class PluginDetailsItem
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public Uri Homepage { get; set; }

        public string Description { get; set; }

        public IEnumerable<ReleaseItem> Releases { get; set; }
    }
}