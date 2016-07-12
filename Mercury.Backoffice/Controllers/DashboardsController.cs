using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Mercury.Backoffice.Models;
using Mercury.Backoffice.DAL;
using Mercury.Backoffice.DAL.Models;
using Mercury.Backoffice.Core.Structure;
using Mercury.Backoffice.Helpers;

namespace Mercury.Backoffice.Controllers
{
    [Authorize]
    public class DashboardsController : Controller
    {

        MercuryBackofficeDbContext _db;
        MercuryBackofficeDbContext dbContext
        {
            get
            {
                if (_db == null)
                    _db = new MercuryBackofficeDbContext();
                return _db;
            }
        }

        public DashboardsController()
        { }

        public DashboardsController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            RaceEvent raceEvent)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            RaceEvent = raceEvent;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private RaceEvent _raceEvent;

        public RaceEvent RaceEvent
        {
            get
            {
                return _raceEvent;
            }
            set
            {
                _raceEvent = value;
            }
        }

        public ActionResult Index()
        {
            try
            {

                var userCount = UserManager.Users.ToList().Count();
                var raceEventCount = dbContext.RaceEvent.Where(x => x.IsActive == true).ToList().Count();
                var raceResultCount = dbContext.RaceResult.Where(x => x.IsActive == true).ToList().Count();
                var raceResultClaimedCount = dbContext.RaceResult.Where(x => x.IsClaimed == true && x.IsActive == true).ToList().Count();
                
                DashboardsViewModel dashboardViewModel = new DashboardsViewModel()
                {
                    TotalRegisteredUsers = userCount,
                    TotalRaceEvent = raceEventCount,
                    TotalRaceResult = raceResultCount,
                    TotalRaceResultClaimed = raceResultClaimedCount,
                    TotalRaceResultClaimedPercentage = (raceResultClaimedCount / raceResultCount) * 100
                };

                return View(dashboardViewModel);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        public ActionResult Dashboard_1()
        {
            return View();
        }

        public ActionResult Dashboard_2()
        {
            return View();
        }

        public ActionResult Dashboard_3()
        {
            return View();
        }
        
        public ActionResult Dashboard_4()
        {
            return View();
        }
        
        public ActionResult Dashboard_4_1()
        {
            return View();
        }

        public ActionResult Dashboard_5()
        {
            return View();
        }

    }
}