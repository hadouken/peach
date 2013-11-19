using System.Collections.Generic;

namespace Peach.Data.Domain
{
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public virtual int Id { get; set; }

        public virtual string ClaimedIdentifier { get; set; }

        public virtual string UserName { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}
