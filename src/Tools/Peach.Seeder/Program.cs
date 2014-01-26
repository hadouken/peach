using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Peach.Core.Runtime;
using Peach.Data.Sql;
using Peach.Seeder.Importers;

namespace Peach.Seeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var lifetimeProvider = new LifetimeProvider();

            // Register modules
            builder.RegisterModule(new SqlModule(lifetimeProvider));

            // Register importers
            builder.RegisterType<BlogPostImporter>().As<IImporter>();

            var container = builder.Build();

            foreach (var importer in container.Resolve<IEnumerable<IImporter>>())
            {
                Console.WriteLine("Running importer {0}", importer.GetType());
                importer.Import();
            }

            Console.ReadKey();
        }
    }

    public class LifetimeProvider : ILifetimeProvider
    {
        public void InstanceScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.InstancePerDependency();
        }

        public void SingletonScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.SingleInstance();
        }

        public void RequestScope<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> context)
        {
            context.InstancePerDependency();
        }
    }
}
