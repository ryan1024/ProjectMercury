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


    }
}
