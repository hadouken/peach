using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Get(string pluginId)
        {
            var plugin = _pluginRepository.GetByName(pluginId);

            if (plugin == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var latestRelease = plugin.Releases.OrderByDescending(r => r.ReleaseDate).FirstOrDefault();

            var dto = new
            {
                name = plugin.Name,
                author = plugin.Author.UserName,
                description = plugin.Description,
                latest_release = (latestRelease == null
                    ? null
                    : new
                    {
                        download_uri = latestRelease.DownloadUri.ToString(),
                        release_date = latestRelease.ReleaseDate,
                        release_notes = latestRelease.ReleaseNotes,
                        version = latestRelease.Version.ToString()
                    })
            };

            return Request.CreateResponse(HttpStatusCode.OK, dto);
        }
    }
}