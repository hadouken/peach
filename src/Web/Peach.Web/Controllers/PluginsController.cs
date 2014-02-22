using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Peach.Core;
using Peach.Core.IO;
using Peach.Core.SemVer;
using Peach.Core.Text;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class PluginsController : PeachController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPluginRepository _pluginRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly IBlobStorage _blobStorage;

        public PluginsController(IConfiguration configuration,
            IUserRepository userRepository,
            IPluginRepository pluginRepository,
            ISlugGenerator slugGenerator,
            IBlobStorage blobStorage)
            : base(configuration, userRepository)
        {
            _userRepository = userRepository;
            _pluginRepository = pluginRepository;
            _slugGenerator = slugGenerator;
            _blobStorage = blobStorage;
        }

        public ActionResult Index()
        {
            var plugins = _pluginRepository.GetAll();
            return View(plugins);
        }

        [Route("plugins/{pluginId}")]
        public ActionResult Details(string pluginId)
        {
            var plugin = _pluginRepository.GetByName(pluginId);

            if (plugin == null)
            {
                return HttpNotFound();
            }

            return View(plugin);
        }

        [Authorize(Roles = Role.PluginDeveloper)]
        [Route("plugins/new")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = Role.PluginDeveloper)]
        [HttpPost]
        [Route("plugins/new")]
        public async Task<ActionResult> New(HttpPostedFileBase package)
        {
            if (package == null)
                return View();

            var userId = Convert.ToInt32(User.Identity.Name);
            var currentUser = _userRepository.GetById(userId);

            byte[] data;

            using (var ms = new MemoryStream())
            {
                package.InputStream.CopyTo(ms);
                data = ms.ToArray();
                package.InputStream.Seek(0, SeekOrigin.Begin);
            }

            using (var zip = ZipFile.Read(package.InputStream))
            {
                var manifest = zip["manifest.json"];

                if (manifest == null)
                {
                    ViewBag.ErrorMessage = "Package does not contain a manifest.json file.";
                    return View();
                }

                using (var streamReader = new StreamReader(manifest.OpenReader()))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var manifestObject = JToken.ReadFrom(jsonReader) as JObject;

                    if (manifestObject == null)
                    {
                        ViewBag.ErrorMessage = "Not a valid manifest.json file.";
                        return View();
                    }

                    var id = manifestObject["id"].Value<string>();
                    var version = manifestObject["version"].Value<string>();

                    // Find plugin
                    var plugin = _pluginRepository.GetByName(id);

                    if (plugin == null)
                    {
                        plugin = new Plugin
                        {
                            Author = currentUser,
                            Description = "",
                            Homepage = new Uri("http://www.hdkn.net/"),
                            Name = id,
                            Slug = _slugGenerator.Generate(id)
                        };

                        _pluginRepository.Insert(plugin);
                    }
                    else
                    {
                        if (plugin.Author != currentUser)
                        {
                            ViewBag.ErrorMessage =
                                "You are not the author of this plugin and can therefore not upload new releases to it.";
                            return View();
                        }
                    }

                    // Check if this version already exists for this plugin
                    if (plugin.Releases.Any(r => r.Version == version))
                    {
                        ViewBag.ErrorMessage = "This version already exists for this plugin";
                        return View();
                    }

                    var fileName = String.Format("{0}-{1}.zip", id, new SemanticVersion(version));
                    var container = _blobStorage.GetContainer("plugins");
                    var blob = await container.CreateBlob(fileName, new MemoryStream(data));

                    plugin.Releases.Add(new PluginRelease
                    {
                        DownloadUri = blob.Uri,
                        Plugin = plugin,
                        ReleaseNotes = string.Empty,
                        Version = version
                    });

                    _pluginRepository.Update(plugin);
                }

            }


            return View();
        }
	}
}