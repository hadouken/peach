using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Octokit;
using Peach.Core;
using Peach.Core.IO;
using Peach.Data;
using Peach.Data.Domain;
using Peach.Web.Models;
using Release = Peach.Data.Domain.Release;

namespace Peach.Web.Controllers
{
    public class DownloadController : PeachController
    {
        private readonly IReleaseRepository _releaseRepository;
        private readonly IBlobStorage _blobStorage;
        private readonly IGitHubClient _gitHubClient;

        public DownloadController(IConfiguration configuration,
            IUserRepository userRepository,
            IReleaseRepository releaseRepository,
            IBlobStorage blobStorage,
            IGitHubClient gitHubClient)
            : base(configuration, userRepository)
        {
            _releaseRepository = releaseRepository;
            _blobStorage = blobStorage;
            _gitHubClient = gitHubClient;
        }

        public ActionResult Index()
        {
            var release = _releaseRepository.GetLatest();
            return View(release);
        }

        [Route("download/v{version}")]
        public Task<ActionResult> RedirectToDownload(string version)
        {
            return GetRedirectForRelease(version);
        }

        [Route("download/latest")]
        public Task<ActionResult> RedirectToDownload()
        {
            return GetRedirectForRelease(null);
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

        private async Task<ActionResult> GetRedirectForRelease(string version)
        {
            var org = Configuration.Settings["GitHub:OrgName"];
            var repo = Configuration.Settings["GitHub:RepoName"];

            var releases = await _gitHubClient.Release.GetAll(org, repo);
            Octokit.Release release = string.IsNullOrEmpty(version)
                ? releases.OrderByDescending(r => r.PublishedAt).FirstOrDefault()
                : releases.FirstOrDefault(r => r.TagName == "v" + version);

            if (release == null) return HttpNotFound("No releases found.");

            var assets = await _gitHubClient.Release.GetAssets(org, repo, release.Id);
            var installer = assets.FirstOrDefault(a => a.Name.EndsWith(".msi"));

            if (installer == null) return HttpNotFound("Latest release has no MSI asset.");

            var handler = new HttpClientHandler {AllowAutoRedirect = false};
            using (var httpClient = new HttpClient(handler))
            {
                var acceptHeader = new MediaTypeWithQualityHeaderValue("application/octet-stream");
                httpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Hadouken", "1.0"));

                var response = await httpClient.GetAsync(installer.Url);

                // Redirect to where the file is
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    return Redirect(response.Headers.Location.AbsoluteUri);
                }
                
                // Return the content
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return File(await response.Content.ReadAsStreamAsync(), installer.ContentType, installer.Name);
                }
            }

            return HttpNotFound();
        }
	}
}