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
    //[Authorize(Roles = "*")]
    [Authorize]
    public class RaceController : Controller
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
        
        public RaceController()
        { }

        public RaceController(RaceEvent raceEvent,
            RaceEventType raceEventType)
        {
            RaceEvent = raceEvent;
            RaceEventType = raceEventType;
        }

        private RaceEvent _raceEvent;
        private RaceEventType _raceEventType;

        public RaceEvent RaceEvent
        {
            get { return _raceEvent; }
            set { _raceEvent = value; }
        }

        public RaceEventType RaceEventType
        {
            get { return _raceEventType; }
            set { _raceEventType = value; }
        }

        #region Race Event
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RaceEventList()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            using (var db = new MercuryBackofficeDbContext())
            {
                var raceEvents = await db.RaceEvent.Where(x => x.EventId.ToString() == id.ToString()).ToListAsync();

                ViewBag.RaceEvents = raceEvents;
                ViewBag.RaceEventCount = raceEvents.Count();

                return View(raceEvents);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RaceEventCreate()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RaceEventCreate(RaceEventViewModel raceEventViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new MercuryBackofficeDbContext())
                    {

                        db.RaceEvent.Add(new RaceEvent()
                        {
                            EventId = Guid.NewGuid().ToString().ToUpper(),
                            EventName = raceEventViewModel.EventName,
                            Description = raceEventViewModel.Description,
                            EventTypeId = raceEventViewModel.EventTypeId,
                            EventOrganiser = raceEventViewModel.EventOrganiser,
                            EventStartOn = raceEventViewModel.EventStartOn,
                            EventEndOn = raceEventViewModel.EventEndOn,
                            EventVenue = raceEventViewModel.EventVenue,
                            Latitude = null,
                            Longitude = null,
                            FacebookURL = null,
                            GooglePlusURL = null,
                            InstagramURL = null,
                            TwitterURL = null,
                            IsCharity = raceEventViewModel.IsCharity,
                            IsTimingChip = raceEventViewModel.IsTimingChip,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = this.User.Identity.Name,
                            UpdatedOn = DateTime.Now,
                            UpdatedBy = this.User.Identity.Name,

                        });

                        var result = await db.SaveChangesAsync();
                    }

                    return RedirectToAction(PageURL.url_Race_RaceEvent_List);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventEdit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                using (var db = new MercuryBackofficeDbContext())
                {
                    var raceEvent = await db.RaceEvent.Where(x => x.EventId.ToString() == id.ToString()).SingleOrDefaultAsync();

                    if (raceEvent == null) { return HttpNotFound(); }

                    RaceEventViewModel raceEventViewModel = new RaceEventViewModel()
                    {
                        EventId = raceEvent.EventId.ToString(),
                        EventName = raceEvent.EventName,
                        Description = raceEvent.Description,
                        EventOrganiser = raceEvent.EventOrganiser,
                        EventTypeId = raceEvent.EventTypeId,
                        EventVenue = raceEvent.EventVenue,
                        EventStartOn = raceEvent.EventStartOn,
                        EventEndOn = raceEvent.EventEndOn,
                        IsTimingChip = raceEvent.IsTimingChip,
                        IsActive = raceEvent.IsActive,
                        IsCharity = raceEvent.IsCharity,
                        Latitude = raceEvent.Latitude,
                        Longitude = raceEvent.Longitude,
                        FacebookURL = raceEvent.FacebookURL,
                        GooglePlusURL = raceEvent.GooglePlusURL,
                        InstagramURL = raceEvent.InstagramURL,
                        TwitterURL = raceEvent.TwitterURL,
                        CreatedOn = raceEvent.CreatedOn,
                        CreatedBy = raceEvent.CreatedBy,
                        UpdatedOn = raceEvent.UpdatedOn,
                        UpdatedBy = raceEvent.UpdatedBy
                    };

                    return View(raceEventViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raceEventViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> RaceEventEdit([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
        public ActionResult RaceEventEdit(RaceEventViewModel raceEventViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var db = new MercuryBackofficeDbContext())
                    {
                        var raceEvent = db.RaceEvent.Where(x => x.EventId.ToString() == raceEventViewModel.EventId.ToString()).SingleOrDefault();

                        if (raceEvent == null) { return HttpNotFound(); }

                        if (raceEvent != null)
                        {
                            raceEvent.EventName = raceEventViewModel.EventName;
                            raceEvent.Description = raceEventViewModel.Description;
                            raceEvent.EventOrganiser = raceEventViewModel.EventOrganiser;
                            raceEvent.EventTypeId = raceEventViewModel.EventTypeId;
                            raceEvent.EventVenue = raceEventViewModel.EventVenue;
                            raceEvent.EventStartOn = raceEventViewModel.EventStartOn;
                            raceEvent.EventEndOn = raceEventViewModel.EventEndOn;
                            raceEvent.IsTimingChip = raceEventViewModel.IsTimingChip;
                            raceEvent.IsActive = raceEventViewModel.IsActive;
                            raceEvent.IsCharity = raceEventViewModel.IsCharity;
                            raceEvent.Latitude = raceEventViewModel.Latitude;
                            raceEvent.Longitude = raceEventViewModel.Longitude;
                            raceEvent.FacebookURL = raceEventViewModel.FacebookURL;
                            raceEvent.GooglePlusURL = raceEventViewModel.GooglePlusURL;
                            raceEvent.InstagramURL = raceEventViewModel.InstagramURL;
                            raceEvent.TwitterURL = raceEventViewModel.TwitterURL;
                            //raceEvent.CreatedOn = DateTime.Now;
                            //raceEvent.CreatedBy = User.Identity.Name;
                            raceEvent.UpdatedOn = DateTime.Now;
                            raceEvent.UpdatedBy = User.Identity.Name;
                        }

                        var result = db.SaveChanges();

                        //if (result)
                        //{
                        return RedirectToAction(PageURL.url_Race_RaceEvent_List);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventDelete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                using (var db = new MercuryBackofficeDbContext())
                {
                    var raceEvent = await db.RaceEvent.Where(x => x.EventId.ToString() == id.ToString()).SingleOrDefaultAsync();

                    if (raceEvent == null) { return HttpNotFound(); }

                    RaceEventViewModel raceEventViewModel = new RaceEventViewModel()
                    {
                        EventId = raceEvent.EventId.ToString(),
                        EventName = raceEvent.EventName,
                        Description = raceEvent.Description,
                        EventOrganiser = raceEvent.EventOrganiser,
                        EventTypeId = raceEvent.EventTypeId,
                        EventVenue = raceEvent.EventVenue,
                        EventStartOn = raceEvent.EventStartOn,
                        EventEndOn = raceEvent.EventEndOn,
                        IsTimingChip = raceEvent.IsTimingChip,
                        IsActive = raceEvent.IsActive,
                        IsCharity = raceEvent.IsCharity,
                        Latitude = raceEvent.Latitude,
                        Longitude = raceEvent.Longitude,
                        FacebookURL = raceEvent.FacebookURL,
                        GooglePlusURL = raceEvent.GooglePlusURL,
                        InstagramURL = raceEvent.InstagramURL,
                        TwitterURL = raceEvent.TwitterURL,
                        //CreatedOn = DateTime.Now,
                        //CreatedBy = User.Identity.Name,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = User.Identity.Name
                    };

                    return View(raceEventViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteRaceEvent"></param>
        /// <returns></returns>
        [HttpPost, ActionName("RaceEventDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RaceEventDeleteConfirmed(string id, string deleteRaceEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    using (var db = new MercuryBackofficeDbContext())
                    {
                        var raceEvent = await db.RaceEvent.Where(x => x.EventId.ToString() == id.ToString()).SingleOrDefaultAsync();

                        if (raceEvent == null) { return HttpNotFound(); }

                        if (raceEvent != null)
                        {
                            db.RaceEvent.Remove(raceEvent);
                            await db.SaveChangesAsync();
                            return RedirectToAction(PageURL.url_Race_RaceEvent_List);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetRaceEventGrid2(int? page, int? limit, string sortBy = "CreatedOn", string direction = "ASC", string searchString = "")
        //public JsonResult GetUserRoleGrid()
        {
            try
            {
                int total = 0;

                var records = new List<RaceEvent>();
                
                if (searchString == string.Empty)
                {
                    //GET TOP 10 only
                    records = dbContext.RaceEvent.OrderBy("CreatedOn", "OrderBy").Paged(1, 200).ToList();
                }
                else if (searchString != null && searchString != string.Empty)
                {
                    searchString = searchString.ToLower();

                    //Description might be null
                    //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();

                    //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                    records = dbContext.RaceEvent.Where(x => x.EventName.ToLower().Contains(searchString) || x.Description.ToLower().Contains(searchString)).ToList();
                }


                //Set the columns to select
                //records = records.Select(x => new { x.Id, x.Name, x.Description }).ToList();

                //Default sorting
                //if (sortBy == null) { sortBy = "CreatedOn"; }

                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc") { direction = GridConstructionHelper.OrderByAsc; }
                    else { direction = GridConstructionHelper.OrderByDesc; }

                    records = GridConstructionHelper.OrderBy(records.AsQueryable(), sortBy, direction).ToList();
                }

                total = records.Count();
                records = GridConstructionHelper.Paged(records.AsQueryable(), page, limit).ToList();
                
                return Json(new { records, total }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return Json(null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sidx"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRaceEventGrid(string sidx, int page, int rows, string sort = "ASC", string searchString = "")
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var records = new List<RaceEvent>();

            if (searchString == string.Empty)
            {
                //GET TOP 200 only
                records = dbContext.RaceEvent.OrderBy("CreatedOn", "OrderBy").Paged(1, 200).ToList();
            }
            else if (searchString != null && searchString != string.Empty)
            {
                searchString = searchString.ToLower();

                //Description might be null
                //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();

                //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                records = dbContext.RaceEvent.Where(x => x.EventName.ToLower().Contains(searchString) || x.Description.ToLower().Contains(searchString)).ToList();
            }

            int totalRecords = records.Count();

            if (sort.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.EventName).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                records = records.OrderBy(s => s.EventName).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            //var jsonData = new
            //{
            //    total = totalPages,
            //    page,
            //    records = totalRecords,
            //    rows = records
            //};

            var jsonData = new
            {
                rows = records.Select(x => new RaceEventViewModel
                {
                    EventId = x.EventId,
                    EventName = x.EventName,
                    Description = x.Description,
                    EventTypeName = x.RaceEventType.EventTypeName,
                    EventStartOn = x.EventStartOn,
                    EventEndOn = x.EventEndOn
                }),
                total = (int)Math.Ceiling((float)totalRecords / (float)rows),
                page,
                records = records.Count(),
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }



        //////////        int pageIndex = Convert.ToInt32(page) - 1;
        //////////        int pageSize = rows;
        //////////        var records = dbContext.RaceEvent.Select(
        //////////                x => new
        //////////                {
        //////////                    x.EventId,
        //////////                    x.EventName,
        //////////                    x.Description,
        //////////                    x.EventOrganiser,
        //////////                    x.EventStartOn,
        //////////                    x.EventEndOn,
        //////////                    x.CreatedBy,
        //////////                    x.CreatedOn
        //////////                });
        //////////        int totalRecords = records.Count();
        //////////        var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
        //////////            if (sort.ToUpper() == "DESC")
        //////////            {
        //////////                records = records.OrderByDescending(s => s.EventName);
        //////////                records = records.Skip(pageIndex* pageSize).Take(pageSize);
        //////////    }
        //////////            else
        //////////            {
        //////////                records = records.OrderBy(s => s.EventName);
        //////////                records = records.Skip(pageIndex* pageSize).Take(pageSize);
        //////////}
        //////////var jsonData = new
        //////////{
        //////////    total = totalPages,
        //////////    page,
        //////////    records = totalRecords,
        //////////    rows = records
        //////////};
        //////////            return Json(jsonData, JsonRequestBehavior.AllowGet);

        #endregion

        #region Race Event Type

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RaceEventTypeList()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventTypeDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new MercuryBackofficeDbContext())
            {
                var raceEventTypes = await db.RaceEventType.Where(x => x.EventTypeId.ToString() == id.ToString()).ToListAsync();

                ViewBag.RaceEventTypes = raceEventTypes;
                ViewBag.RaceEventTypeCount = raceEventTypes.Count();

                return View(raceEventTypes);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RaceEventTypeCreate()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raceEventViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RaceEventTypeCreate(RaceEventTypeViewModel raceEventTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new MercuryBackofficeDbContext())
                    {

                        db.RaceEventType.Add(new RaceEventType()
                        {
                            //EventTypeId = Incremental
                            EventTypeName = raceEventTypeViewModel.EventTypeName,
                            Description = raceEventTypeViewModel.Description,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = this.User.Identity.Name,
                            UpdatedOn = DateTime.Now,
                            UpdatedBy = this.User.Identity.Name,

                        });

                        var result = await db.SaveChangesAsync();
                    }

                    return RedirectToAction(PageURL.url_Race_RaceEventType_List);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventTypeEdit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                using (var db = new MercuryBackofficeDbContext())
                {
                    var raceEventType = await db.RaceEventType.Where(x => x.EventTypeId.ToString() == id.ToString()).SingleOrDefaultAsync();

                    if (raceEventType == null) { return HttpNotFound(); }

                    RaceEventTypeViewModel raceEventTypeViewModel = new RaceEventTypeViewModel()
                    {
                        EventTypeId = raceEventType.EventTypeId,
                        EventTypeName = raceEventType.EventTypeName,
                        Description = raceEventType.Description,
                        IsActive = raceEventType.IsActive,
                        CreatedOn = raceEventType.CreatedOn,
                        CreatedBy = raceEventType.CreatedBy,
                        UpdatedOn = raceEventType.UpdatedOn,
                        UpdatedBy = raceEventType.UpdatedBy
                    };

                    return View(raceEventTypeViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raceEventTypeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> RaceEventTypeEdit([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
        public ActionResult RaceEventTypeEdit(RaceEventTypeViewModel raceEventTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var db = new MercuryBackofficeDbContext())
                    {
                        var raceEventType = db.RaceEventType.Where(x => x.EventTypeId.ToString() == raceEventTypeViewModel.EventTypeId.ToString()).SingleOrDefault();

                        if (raceEventType == null) { return HttpNotFound(); }

                        if (raceEventType != null)
                        {
                            raceEventType.EventTypeName = raceEventTypeViewModel.EventTypeName;
                            raceEventType.Description = raceEventTypeViewModel.Description;
                            raceEventType.IsActive = raceEventTypeViewModel.IsActive;
                            //raceEventType.CreatedOn = DateTime.Now;
                            //raceEventType.CreatedBy = User.Identity.Name;
                            raceEventType.UpdatedOn = DateTime.Now;
                            raceEventType.UpdatedBy = User.Identity.Name;
                        }

                        var result = db.SaveChanges();

                        //if (result)
                        //{
                        return RedirectToAction(PageURL.url_Race_RaceEventType_List);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> RaceEventTypeDelete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                using (var db = new MercuryBackofficeDbContext())
                {
                    var raceEventType = await db.RaceEventType.Where(x => x.EventTypeId.ToString() == id.ToString()).SingleOrDefaultAsync();

                    if (raceEventType == null) { return HttpNotFound(); }

                    RaceEventTypeViewModel raceEventTypeViewModel = new RaceEventTypeViewModel()
                    {
                        EventTypeId = raceEventType.EventTypeId,
                        EventTypeName = raceEventType.EventTypeName,
                        Description = raceEventType.Description,
                        IsActive = raceEventType.IsActive,
                        //CreatedOn = DateTime.Now,
                        //CreatedBy = User.Identity.Name,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = User.Identity.Name
                    };

                    return View(raceEventTypeViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteRaceEventType"></param>
        /// <returns></returns>
        [HttpPost, ActionName("RaceEventTypeDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RaceEventTypeDeleteConfirmed(string id, string deleteRaceEventType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    using (var db = new MercuryBackofficeDbContext())
                    {
                        var raceEventType = await db.RaceEventType.Where(x => x.EventTypeId.ToString() == id.ToString()).SingleOrDefaultAsync();

                        if (raceEventType == null) { return HttpNotFound(); }

                        if (raceEventType != null)
                        {
                            db.RaceEventType.Remove(raceEventType);
                            await db.SaveChangesAsync();
                            return RedirectToAction(PageURL.url_Race_RaceEventType_List);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sidx"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRaceEventTypeGrid(string sidx, int page, int rows, string sort = "ASC", string searchString = "")
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var records = new List<RaceEventType>();

            if (searchString == string.Empty)
            {
                //GET TOP 200 only
                records = dbContext.RaceEventType.OrderBy("CreatedOn", "OrderBy").Paged(1, 200).ToList();
            }
            else if (searchString != null && searchString != string.Empty)
            {
                searchString = searchString.ToLower();

                //Description might be null
                //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();

                //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                records = dbContext.RaceEventType.Where(x => x.EventTypeName.ToLower().Contains(searchString) || x.Description.ToLower().Contains(searchString)).ToList();
            }


            int totalRecords = records.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.EventTypeName).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                records = records.OrderBy(s => s.EventTypeName).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            var jsonData = new
            {
                rows = records.Select(x => new RaceEventTypeViewModel
                {
                    EventTypeId = x.EventTypeId,
                    EventTypeName = x.EventTypeName,
                    Description = x.Description
                }),
                total = (int)Math.Ceiling((float)totalRecords / (float)rows),
                page,
                records = records.Count(),
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //public List<RaceEventType> GetRaceEventTypeLOV()
        //{
        //    var records = dbContext.RaceEventType.Where(x => x.IsActive == true).OrderBy("EventTypeName", "OrderBy").ToList();

        //    if (records != null) { return new List<RaceEventType>(); }

        //    return records;
        //}

        public static List<RaceEventType> GetRaceEventTypeLOV()
        {
            using (var db = new MercuryBackofficeDbContext())
            {
                var records = db.RaceEventType.Where(x => x.IsActive == true).OrderBy("EventTypeName", "OrderBy").ToList();

                if (records == null) { return new List<RaceEventType>(); }

                return records;
            }
        }

        #endregion

        #region Race Result

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RaceResultList(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewBag.EventId = id;

                using (var db = new MercuryBackofficeDbContext())
                {
                    var raceEvent = db.RaceEvent.Where(x => x.EventId.ToString() == id.ToString()).SingleOrDefault();

                    if (raceEvent == null) { return HttpNotFound(); }

                    RaceEventViewModel raceEventViewModel = new RaceEventViewModel()
                    {
                        EventId = raceEvent.EventId.ToString(),
                        EventName = raceEvent.EventName,
                        Description = raceEvent.Description,
                        EventOrganiser = raceEvent.EventOrganiser,
                        EventTypeId = raceEvent.EventTypeId,
                        EventVenue = raceEvent.EventVenue,
                        EventStartOn = raceEvent.EventStartOn,
                        EventEndOn = raceEvent.EventEndOn,
                        IsTimingChip = raceEvent.IsTimingChip,
                        IsActive = raceEvent.IsActive,
                        IsCharity = raceEvent.IsCharity,
                        Latitude = raceEvent.Latitude,
                        Longitude = raceEvent.Longitude,
                        FacebookURL = raceEvent.FacebookURL,
                        GooglePlusURL = raceEvent.GooglePlusURL,
                        InstagramURL = raceEvent.InstagramURL,
                        TwitterURL = raceEvent.TwitterURL,
                        CreatedOn = raceEvent.CreatedOn,
                        CreatedBy = raceEvent.CreatedBy,
                        UpdatedOn = raceEvent.UpdatedOn,
                        UpdatedBy = raceEvent.UpdatedBy
                    };

                    return View(raceEventViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sidx"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRaceResultGrid(string eventId, string sidx, int page, int rows, string sort = "ASC", string searchString = "")
        {
            if (eventId == null) { return null; }

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var records = new List<RaceResult>();

            if (searchString == string.Empty)
            {
                //GET TOP 200 only
                records = dbContext.RaceResult.Where(x => x.EventId == eventId).OrderBy("BIB", "OrderBy").Paged(1, 200).ToList();
            }
            else if (searchString != null && searchString != string.Empty)
            {
                searchString = searchString.ToLower();

                //Description might be null
                //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();

                //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                records = dbContext.RaceResult.Where(x => x.EventId == eventId && x.BIB.ToLower().Contains(searchString) || x.FullName.ToLower().Contains(searchString)).ToList();
            }


            int totalRecords = records.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                records = records.OrderByDescending(s => s.BIB).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                records = records.OrderBy(s => s.BIB).ToList();
                records = records.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            var jsonData = new
            {
                rows = records.Select(x => new RaceResultViewModel
                {
                    ResultId = x.ResultId,
                    EventId = x.EventId,
                    EventName = x.RaceEvent.EventName,
                    BIB = x.BIB,
                    FullName  = x.FullName,
                    CategoryDesc = x.CategoryDesc,
                    Nationality = x.Nationality,
                    RankingByCategory = x.RankingByCategory,
                    GunTime = x.GunTime
                }),
                total = (int)Math.Ceiling((float)totalRecords / (float)rows),
                page,
                records = records.Count(),
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}