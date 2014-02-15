using System;
using System.IO;

namespace Peach.Core.IO
{
    public interface IBlob
    {
        Uri Uri { get; }

        long Size { get; }

        string Name { get; }

        Stream OpenRead();
    }
}
