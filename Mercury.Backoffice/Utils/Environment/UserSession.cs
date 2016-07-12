using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Mercury.Backoffice.Core.Structure;
using Mercury.Backoffice.Models;

namespace Mercury.Backoffice.Utils.Environment
{
    public class UserSession : IUserSession
    {
        private T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private void SetInSession(string key, Foundation value)
        {
            SetInSession(key, (object)value);
            //if (!string.IsNullOrEmpty(value.UserName))
            //    FormsAuthentication.SetAuthCookie(value.UserName, value.IsRememberMe);
        }

        public Foundation Foundation
        {
            get
            {
                if (GetFromSession<Foundation>("Foundation") == null)
                {
                    ClearFoundation();
                    Foundation = new Foundation();
                }

                return GetFromSession<Foundation>("Foundation");
            }
            set { SetInSession("Foundation", value); }
        }

        public void ClearFoundation()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            Foundation = new Foundation();
        }

        public static void SetUserProfile(IPrincipal user)
        {
            if (user == null) { return; }

            var foundation = new Mercury.Backoffice.Core.Structure.Foundation();

            foundation.ConstructNavigationData("#" + GetUserRole(user) + "#");
            Mercury.Backoffice.Utils.Environment.Assembly.SessionContext.Foundation = foundation;

            //////////var userIdentity = (ClaimsIdentity)user.Identity;
            //////////var claims = userIdentity.Claims;
            //////////var roleClaimType = userIdentity.RoleClaimType;
            ////////////var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            //////////if ((claims != null) && (claims.Count() > 0))
            //////////{
            //////////    foundation.Roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            //////////    if (foundation.Roles != null && foundation.Roles.Count > 0)
            //////////    {
            //////////        foundation.ConstructNavigationData("#" + foundation.Roles.SingleOrDefault().Value.ToString() + "#");

            //////////        Mercury.Backoffice.Utils.Environment.Assembly.SessionContext.Foundation = foundation;
            //////////    }
            //////////}
        }

        public static void SetUserProfile(ApplicationUser user)
        {
            if (user == null) { return; }

            var foundation = new Mercury.Backoffice.Core.Structure.Foundation();

            foundation.ConstructNavigationData("#" + GetUserRole(user) + "#");
            Mercury.Backoffice.Utils.Environment.Assembly.SessionContext.Foundation = foundation;

            //////////var userIdentity = (ClaimsIdentity)user.Identity;
            //////////var claims = userIdentity.Claims;
            //////////var roleClaimType = userIdentity.RoleClaimType;
            ////////////var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            //////////if ((claims != null) && (claims.Count() > 0))
            //////////{
            //////////    foundation.Roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            //////////    if (foundation.Roles != null && foundation.Roles.Count > 0)
            //////////    {
            //////////        foundation.ConstructNavigationData("#" + foundation.Roles.SingleOrDefault().Value.ToString() + "#");

            //////////        Mercury.Backoffice.Utils.Environment.Assembly.SessionContext.Foundation = foundation;
            //////////    }
            //////////}
        }

        public static string GetUserRole(IPrincipal user)
        {
            string role = string.Empty;

            if (user == null) { return string.Empty; }

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            
            
            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Identity.GetUserId());

            foreach (var item in rolesForUser.ToList())
            {
                //IF MULTIPLE ROLE, TAKE LAST ROLE
                role = item.ToString();
            }

            return role;
        }

        public static string GetUserRole(ApplicationUser user)
        {
            string role = string.Empty;

            if (user == null) { return string.Empty; }

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();


            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);

            foreach (var item in rolesForUser.ToList())
            {
                //IF MULTIPLE ROLE, TAKE LAST ROLE
                role = item.ToString();
            }

            return role;
        }


    }
}
