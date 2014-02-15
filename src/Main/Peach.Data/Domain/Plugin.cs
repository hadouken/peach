using System;

namespace Peach.Data.Domain
{
    public class Plugin
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Slug { get; set; }

        public virtual User Author { get; set; }

        public virtual Uri Homepage { get; set; }

        public virtual string Description { get; set; }
    }
}
