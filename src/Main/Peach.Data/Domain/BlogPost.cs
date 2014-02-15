using System;

namespace Peach.Data.Domain
{
    public class BlogPost
    {
        public virtual int Id { get; set; }

        public virtual User User { get; set; }

        public virtual string Title { get; set; }

        public virtual string Slug { get; set; }

        public virtual DateTime PublishedDate { get; set; }

        public virtual string Content { get; set; }
    }
}
