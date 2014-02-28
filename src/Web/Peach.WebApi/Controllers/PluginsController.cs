using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Peach.Data;
using Peach.WebApi.Models;

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
        public IEnumerable<PluginListItem> Get()
        {
            var plugins = _pluginRepository.GetAll();

            return (from plugin in plugins
                let latestRelease = (from release in plugin.Releases
                    orderby release.ReleaseDate descending
                    select new ReleaseItem
                    {
                        DownloadUri = release.DownloadUri,
                        ReleaseDate = release.ReleaseDate,
                        Version = release.Version
                    }).FirstOrDefault()
                select new PluginListItem
                {
                    Author = plugin.Author.UserName,
                    Description = plugin.Description,
                    Homepage = plugin.Homepage,
                    Id = plugin.Name,
                    LatestRelease = latestRelease
                });
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

            var dto = new PluginDetailsItem
            {
                Author = plugin.Author.UserName,
                Description = plugin.Description,
                Homepage = plugin.Homepage,
                Id = plugin.Name,
                Releases = (from release in plugin.Releases
                    orderby release.ReleaseDate descending
                    select new ReleaseItem
                    {
                        DownloadUri = release.DownloadUri,
                        ReleaseDate = release.ReleaseDate,
                        Version = release.Version
                    })
            };

            return Request.CreateResponse(HttpStatusCode.OK, dto);
        }
    }
}