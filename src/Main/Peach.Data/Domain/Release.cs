using System;
using System.Collections.Generic;

namespace Peach.Data.Domain
{
    public class Release
    {
        public Release()
        {
            Files = new List<ReleaseFile>();
            ReleaseDate = DateTime.Now;
        }

        public virtual int Id { get; set; }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual string Version { get; set; }

        public virtual string ReleaseNotes { get; set; }

        public virtual IList<ReleaseFile> Files { get; set; }
    }

    public class ReleaseFile
    {
        public virtual int Id { get; set; }

        public virtual Release Release { get; set; }

        public virtual Uri DownloadUri { get; set; }
    }
}
