using System;
using Peach.Core.SemVer;

namespace Peach.Data.Domain
{
    public class PluginRelease
    {
        public PluginRelease()
        {
            ReleaseDate = DateTime.UtcNow;
        }

        public virtual int Id { get; set; }

        public virtual Plugin Plugin { get; set; }

        public virtual SemanticVersion Version { get; set; }

        public virtual Uri DownloadUri { get; set; }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual string ReleaseNotes { get; set; }
    }
}
