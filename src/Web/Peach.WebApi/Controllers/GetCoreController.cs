using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Peach.Data;

namespace Peach.WebApi.Controllers
{
    public class GetCoreController : ApiController
    {
        private readonly IPluginRepository _pluginRepository;

        public GetCoreController(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository;
        }

        [Route("get-core")]
        public IEnumerable<Uri> Get()
        {
            var corePlugins = _pluginRepository.GetAll().Where(p => p.Name.StartsWith("core."));

            return (from plugin in corePlugins
                let latestRelease = plugin.Releases.OrderByDescending(r => r.ReleaseDate).FirstOrDefault()
                where latestRelease != null
                select latestRelease.DownloadUri);
        }
    }
}
