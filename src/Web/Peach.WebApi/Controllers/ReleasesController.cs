using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Peach.Data;
using Peach.WebApi.Models;

namespace Peach.WebApi.Controllers
{
    public class ReleasesController : ApiController
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleasesController(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public IEnumerable<ReleaseItem> Get()
        {
            return (from release in _releaseRepository.GetAll()
                orderby release.ReleaseDate descending
                let installer = release.Files.FirstOrDefault(f => f.DownloadUri.LocalPath.EndsWith(".msi"))
                where installer != null
                select new ReleaseItem
                {
                    ReleaseDate = release.ReleaseDate,
                    DownloadUri = installer.DownloadUri,
                    Version = release.Version
                });
        } 
    }
}