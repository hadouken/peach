using System.Collections.Specialized;

namespace Peach.Core
{
    public interface IConfiguration
    {
        NameValueCollection Settings { get; }
    }
}
