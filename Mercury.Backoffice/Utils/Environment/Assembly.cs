using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Backoffice.Utils.Environment
{
    public class Assembly
    {
        private static readonly IUserSession _sessionContext = new UserSession();

        public static IUserSession SessionContext { get { return _sessionContext; } }
    }
}