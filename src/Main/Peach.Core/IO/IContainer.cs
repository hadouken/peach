using System.IO;
using System.Threading.Tasks;

namespace Peach.Core.IO
{
    public interface IContainer
    {
        string Name { get; }

        IBlob GetBlob(string name);

        void DeleteBlob(string name);

        Task<IBlob> CreateBlob(string name, Stream inputStream);
    }
}
