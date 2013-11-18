using Autofac;
using Peach.Data.Sql.Repositories;

namespace Peach.Data.Sql
{
    public class SqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlogRepository>().AsImplementedInterfaces();
            builder.RegisterType<PluginRepository>().AsImplementedInterfaces();
        }
    }
}
