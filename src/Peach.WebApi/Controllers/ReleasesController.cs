using System.Collections.Generic;
using System.Web.Http;

namespace Peach.WebApi.Controllers
{
    public class ReleasesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new[] {"1", "2"};
        } 
    }
}