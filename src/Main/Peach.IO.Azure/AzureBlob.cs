using System;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Peach.Core.IO;

namespace Peach.IO.Azure
{
    public class AzureBlob : IBlob
    {
        private readonly ICloudBlob _blob;

        public AzureBlob(ICloudBlob blob)
        {
            _blob = blob;
        }

        public Uri Uri
        {
            get { return _blob.Uri; }
        }

        public long Size
        {
            get { return _blob.Properties.Length; }
        }

        public string Name
        {
            get { return _blob.Name; }
        }

        public Stream OpenRead()
        {
            return _blob.OpenRead();
        }
    }
}
