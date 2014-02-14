using System.Web.Mvc;
using Peach.Data;
using Peach.Data.Domain;
using Peach.Web.Models;

namespace Peach.Web.Controllers
{
    public class DownloadController : PeachController
    {
        private readonly IReleaseRepository _releaseRepository;

        public DownloadController(IUserRepository userRepository, IReleaseRepository releaseRepository)
            : base(userRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public ActionResult Index()
        {
            var release = _releaseRepository.GetLatest();
            return View(release);
        }

        public ActionResult New()
        {
            return View(new NewReleaseDto());
        }

        [HttpPost]
        public ActionResult New(NewReleaseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var release = new Release
            {
                DownloadUri = dto.DownloadUri,
                Version = dto.Version
            };

            _releaseRepository.Insert(release);

            return RedirectToAction("Index");
        }
	}
}