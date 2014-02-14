using System;

namespace Peach.Data.Domain
{
    public class Release
    {
        public Release()
        {
            ReleaseDate = DateTime.Now;
        }

        public virtual int Id { get; set; }

        public virtual Uri DownloadUri { get; set; }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual string Version { get; set; }
    }
}
