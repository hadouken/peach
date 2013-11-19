using System;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Peach.Data;

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
                    throw new Exception("Authenticated");

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