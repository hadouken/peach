using System;
using System.Web.Mvc;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public abstract class PeachController : Controller
    {
        private readonly IUserRepository _userRepository;

        protected PeachController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!User.Identity.IsAuthenticated) return;

            var userId = Convert.ToInt32(User.Identity.Name);
            var currentUser = _userRepository.GetById(userId);

            filterContext.Controller.ViewBag.UserName = currentUser.UserName;
        }
    }
}