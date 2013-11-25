using System;

namespace Peach.Data.Domain
{
    public class Feature
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Slug { get; set; }

        public virtual Uri ImageUri { get; set; }

        public virtual string TagLine { get; set; }

        public virtual string ShortContent { get; set; }

        public virtual string Content { get; set; }
    }
}
