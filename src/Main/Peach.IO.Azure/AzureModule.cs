using System.Configuration;
using Autofac;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Peach.Core.Runtime;

namespace Peach.IO.Azure
{
    public class AzureModule : Module
    {
        private readonly ILifetimeProvider _lifetimeProvider;

        public AzureModule(ILifetimeProvider lifetimeProvider)
        {
            _lifetimeProvider = lifetimeProvider;
        }

        protected override void Load(ContainerBuilder builder)
        {
            _lifetimeProvider.InstanceScope(builder.Register(CreateCloudBlobClient));
            _lifetimeProvider.SingletonScope(builder.RegisterType<AzureBlobStorage>().AsImplementedInterfaces());
        }

        private static CloudBlobClient CreateCloudBlobClient(IComponentContext componentContext)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HadoukenStorage"].ConnectionString;
            var account = CloudStorageAccount.Parse(connectionString);

            return account.CreateCloudBlobClient();
        }
    }
}
