using System;
using System.Web.Mvc;
using Peach.Core;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public abstract class PeachController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        protected PeachController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // Set tracking code
            var trackingCode = _configuration.Settings["GoogleAnalytics:TrackingCode"];
            filterContext.Controller.ViewBag.TrackingCode = trackingCode;

            if (!User.Identity.IsAuthenticated) return;

            var userId = Convert.ToInt32(User.Identity.Name);
            var currentUser = _userRepository.GetById(userId);

            filterContext.Controller.ViewBag.UserName = currentUser.UserName;
        }
    }
}