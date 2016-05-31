using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercury.Backoffice.Core.Structure;

namespace Mercury.Backoffice.Utils.Environment
{
    public interface IUserSession
    {
        Foundation Foundation { get; set; }

        void ClearFoundation();
    }
}
