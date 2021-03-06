﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Peach.Core;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class UsersController : PeachController
    {
        private static readonly OpenIdRelyingParty OpenId = new OpenIdRelyingParty();
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UsersController(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository)
            : base(configuration, userRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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

            if (_userRepository.GetAll().Any(u => string.Equals(userName, u.UserName, StringComparison.InvariantCultureIgnoreCase)))
                return View();

            var user = new User
            {
                ClaimedIdentifier = Session["ClaimedIdentifier"].ToString(),
                UserName = userName
            };

            // The first user should be added to all roles
            if (_userRepository.Count() == 0)
            {
                user.Roles.AddRange(_roleRepository.GetAll());
            }

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