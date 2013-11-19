using System;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class UsersController : Controller
    {
        private static readonly OpenIdRelyingParty OpenId = new OpenIdRelyingParty();
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult New()
        {
            if (Session["ClaimedIdentifier"] == null)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        public ActionResult New(string userName)
        {
            if (Session["ClaimedIdentifier"] == null)
                return HttpNotFound();

            if (String.IsNullOrEmpty(userName))
                return View();

            userName = userName.Trim();

            if (_userRepository.GetByUserName(userName) != null)
                return View();

            var user = new User
            {
                ClaimedIdentifier = Session["ClaimedIdentifier"].ToString(),
                UserName = userName
            };

            _userRepository.Insert(user);
            Session.Remove("ClaimedIdentifier");

            FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Authenticate(string returnUrl)
        {
            var response = OpenId.GetResponse();

            if (response == null)
            {
                Identifier identifier;

                if (Identifier.TryParse(Request.Form["openid_identifier"], out identifier))
                {
                    try
                    {
                        var request = OpenId.CreateRequest(identifier);
                        return request.RedirectingResponse.AsActionResultMvc5();
                    }
                    catch (ProtocolException protocolException)
                    {
                        TempData["Flash"] = protocolException.Message;
                        return RedirectToAction("SignIn");
                    }
                }

                TempData["Flash"] = "Invalid identifier.";
                return RedirectToAction("SignIn");
            }

            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    // Find user with claimed identifier. If null, redirect to /users/new
                    var user = _userRepository.GetByClaimedIdentifier(response.ClaimedIdentifier);

                    if (user == null)
                    {
                        Session.Add("ClaimedIdentifier", response.ClaimedIdentifier);
                        return RedirectToAction("New");
                    }

                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

                    if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");

                case AuthenticationStatus.Canceled:
                    TempData["Flash"] = "Canceled at provider.";
                    return RedirectToAction("SignIn");

                case AuthenticationStatus.Failed:
                    TempData["Flash"] = response.Exception.Message;
                    return RedirectToAction("SignIn");
            }

            return HttpNotFound();
        }
    }
}