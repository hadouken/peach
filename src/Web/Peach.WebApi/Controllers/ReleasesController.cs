using System.Collections.Generic;
using System.Web.Http;
using Peach.Data;

namespace Peach.WebApi.Controllers
{
    public class ReleasesController : ApiController
    {
        private readonly IReleaseRepository _releaseRepository;

        public ReleasesController(IReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public IEnumerable<string> Get()
        {
            return new[] {"1", "2"};
        } 
    }
}