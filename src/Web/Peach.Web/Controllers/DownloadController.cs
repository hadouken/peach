using System.Threading.Tasks;
using System.Web.Mvc;
using Peach.Core;
using Peach.Core.IO;
using Peach.Data;
using Peach.Data.Domain;
using Peach.Web.Models;

namespace Peach.Web.Controllers
{
    public class DownloadController : PeachController
    {
        private readonly IReleaseRepository _releaseRepository;
        private readonly IBlobStorage _blobStorage;

        public DownloadController(IConfiguration configuration,
            IUserRepository userRepository,
            IReleaseRepository releaseRepository,
            IBlobStorage blobStorage)
            : base(configuration, userRepository)
        {
            _releaseRepository = releaseRepository;
            _blobStorage = blobStorage;
        }

        public ActionResult Index()
        {
            var release = _releaseRepository.GetLatest();
            return View(release);
        }

        [Authorize(Roles = Role.Administrator)]
        public ActionResult New()
        {
            return View(new NewReleaseDto());
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult> New(NewReleaseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var release = new Release
            {
                ReleaseNotes = dto.ReleaseNotes,
                Version = dto.Version
            };

            var container = _blobStorage.GetContainer("releases");
            foreach (var file in dto.AttachedFiles)
            {
                var blob = await container.CreateBlob(file.FileName, file.InputStream);

                release.Files.Add(new ReleaseFile
                {
                    DownloadUri = blob.Uri,
                    Release = release
                });
            }

            _releaseRepository.Insert(release);

            return RedirectToAction("Index");
        }
	}
}