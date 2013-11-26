namespace Peach.Data.Domain
{
    public class Role
    {
        public const string Administrator = "Administrator";
        public const string PluginDeveloper = "Plugin developer";
        public const string ContentWriter = "Content writer";

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }
}