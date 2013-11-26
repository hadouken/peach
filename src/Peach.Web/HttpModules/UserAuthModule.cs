using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Peach.Data;

namespace Peach.Web.HttpModules
{
    public class UserAuthModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
        }

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return;
            }

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null)
            {
                return;
            }

            var userRepository = DependencyResolver.Current.GetService<IUserRepository>();

            var currentUser = userRepository.GetById(Convert.ToInt32(ticket.Name));
            if (currentUser == null)
            {
                return;
            }

            var identity = new GenericIdentity(ticket.Name);
            var principal = new GenericPrincipal(identity, currentUser.Roles.Select(r => r.Name).ToArray());

            HttpContext.Current.User = principal;
        }
    }
}