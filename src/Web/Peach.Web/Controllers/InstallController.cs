using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Octokit;
using Peach.Core;

namespace Peach.Web.Controllers
{
    public class InstallController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IGitHubClient _gitHubClient;

        public InstallController(IConfiguration configuration, IGitHubClient gitHubClient)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (gitHubClient == null) throw new ArgumentNullException("gitHubClient");
            _configuration = configuration;
            _gitHubClient = gitHubClient;
        }

        [Route("install.ps1")]
        public async Task<ActionResult> Index()
        {
            if (_configuration.Settings["GitHub:InstallGistId"] == null)
            {
                throw new InvalidOperationException("No Gist ID specified.");
            }

            var gistId = _configuration.Settings["GitHub:InstallGistId"];
            var gist = await _gitHubClient.Gist.Get(gistId);

            return Content(gist.Files["install.ps1"].Content, "text/plain");
        }
	}
}