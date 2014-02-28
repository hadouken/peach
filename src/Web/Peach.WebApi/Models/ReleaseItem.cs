using System;

namespace Peach.WebApi.Models
{
    public class ReleaseItem
    {
        public Uri DownloadUri { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Version { get; set; }
    }
}