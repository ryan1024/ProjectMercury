using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using Mercury.Backoffice.DAL;
using Mercury.Backoffice.DAL.Models;

namespace Mercury.Backoffice.Core.Structure
{
    public class Foundation
    {
        public List<Menu> MenuListing { get; private set; }
        public List<MenuItem> MenuItemListing { get; private set; }

        public List<Claim> Roles { get; set; }

        public void ConstructNavigationData(string roleId)
        {
            MenuListing = new List<Menu>();
            MenuItemListing = new List<MenuItem>();

            if (roleId == string.Empty) { return; }

            using (var db = new MercuryBackofficeDbContext())
            {
                MenuListing = db.Menu.Where(x => x.Roles.Contains(roleId)).ToList();
                MenuItemListing = db.MenuItem.Where(x => x.Roles.Contains(roleId)).ToList();
            }

            return;
        }

    }
}
