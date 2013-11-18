using System.Collections.Generic;
using System.Web.Http;
using Peach.Data;

namespace Peach.WebApi.Controllers
{
    public class PluginsController : ApiController
    {
        private readonly IPluginRepository _pluginRepository;

        public PluginsController(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository;
        }

        // GET plugins/
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET plugins/plugin.name
        [Route("plugins/{pluginId}")]
        public string Get(string pluginId)
        {
            return "value";
        }
    }
}