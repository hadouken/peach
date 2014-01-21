using System.Collections.Specialized;
using System.Configuration;

namespace Peach.Core
{
    public class AppConfigConfiguration : IConfiguration
    {
        public NameValueCollection Settings
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}