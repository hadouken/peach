using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Peach.Core.IO;

namespace Peach.IO.Azure
{
    public class AzureContainer : IContainer
    {
        private readonly CloudBlobContainer _container;

        public AzureContainer(CloudBlobContainer container)
        {
            _container = container;
        }

        public string Name { get { return _container.Name; } }

        public IBlob GetBlob(string name)
        {
            var blob = _container.GetBlobReferenceFromServer(name);
            return new AzureBlob(blob);
        }

        public void DeleteBlob(string name)
        {
            var blob = _container.GetBlobReferenceFromServer(name);
            blob.DeleteIfExists();
        }

        public async Task<IBlob> CreateBlob(string name, Stream inputStream)
        {
            var blob = _container.GetBlockBlobReference(name);
            await blob.UploadFromStreamAsync(inputStream);

            return new AzureBlob(blob);
        }
    }
}
