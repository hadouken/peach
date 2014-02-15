using Microsoft.WindowsAzure.Storage.Blob;
using Peach.Core.IO;

namespace Peach.IO.Azure
{
    public class AzureBlobStorage : IBlobStorage
    {
        private readonly CloudBlobClient _blobClient;

        public AzureBlobStorage(CloudBlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public IContainer GetContainer(string name)
        {
            var container = _blobClient.GetContainerReference(name);
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            return new AzureContainer(container);
        }
    }
}
