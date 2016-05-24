using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercury.Backoffice.DAL.Models;

namespace Mercury.Backoffice.Core.Session
{
    public class User
    {
        public List<Menu> MenuListing(string roleId)
        {
            using (var db = new MercuryBackofficeDbContext())
            {
                var menuListing = db.Menu.
            }
            

            return 
            

        }
    }
}
