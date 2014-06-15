using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Octokit;
using Peach.Core;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class DownloadController : PeachController
    {
        private readonly IGitHubClient _gitHubClient;

        public DownloadController(IConfiguration configuration,
            IUserRepository userRepository,
            IGitHubClient gitHubClient)
            : base(configuration, userRepository)
        {
            _gitHubClient = gitHubClient;
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

        private async Task<ActionResult> GetRedirectForRelease(string version)
        {
            var org = Configuration.Settings["GitHub:OrgName"];
            var repo = Configuration.Settings["GitHub:RepoName"];

            var releases = await _gitHubClient.Release.GetAll(org, repo);
            var release = string.IsNullOrEmpty(version)
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