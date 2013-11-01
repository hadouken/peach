namespace Peach.Data.Domain
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string ClaimedIdentifier { get; set; }

        public virtual string UserName { get; set; }
    }
}
