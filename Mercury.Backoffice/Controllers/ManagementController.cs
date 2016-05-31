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
    public class ManagementController : Controller
    {
        public ManagementController()
        {
        }

        public ManagementController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
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

        #region "User Roles"

        //
        // GET: /Roles/
        public ActionResult RoleList()
        {
            return View(RoleManager.Roles);
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> RoleDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult RoleCreate()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> RoleCreate(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole(roleViewModel.Name);

                // Save the new Description property:
                role.Description = roleViewModel.Description;
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction(PageURL.url_Management_Role_List);
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> RoleEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };

            // Update the new Description property for the ViewModel:
            roleModel.Description = role.Description;

            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();

            return View(roleModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleEdit([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;

                // Update the new Description property:
                role.Description = roleModel.Description;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction(PageURL.url_Management_Role_List);
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> RoleDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();

            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("RoleDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleDeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction(PageURL.url_Management_Role_List);
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetUserRoleGrid(int? page, int? limit, string sortBy = null, string direction = "ASC", string searchString = null)
        //public JsonResult GetUserRoleGrid()
        {
            int total;
            var records = RoleManager.Roles.ToList();

            if (searchString != null)
            {
                searchString = searchString.ToLower();

                //Description might be null
                //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();
                
                //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                records = records.Where(x => x.Name.ToLower().Contains(searchString) || x.Description.ToLower().Contains(searchString)).ToList();
            }

            //Set the columns to select
            //records = records.Select(x => new { x.Id, x.Name, x.Description }).ToList();

            //Default sorting
            if (sortBy == null) { sortBy = "Name"; }

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


        #endregion

        #region "Users"

        //
        // GET: /Users/
        public async Task<ActionResult> UserList()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> UserDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            using (var db = new MercuryBackofficeDbContext())
            {
                var userExt = db.AspNetUsersExtension.Where(x => x.UserId == id).SingleOrDefault();

                //if (userExt !=null)
                //{
                //    user.LastLoginOn = (DateTime)userExt.LastLoginOn;
                //    user.CreatedOn = (DateTime)userExt.CreatedOn;
                //    user.CreatedBy = userExt.CreatedBy;
                //    user.UpdatedOn = (DateTime)userExt.UpdatedOn;
                //    user.UpdatedBy = userExt.UpdatedBy;
                //}
            }


            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);            

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> UserCreate()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.OrderBy(x => x.Name).ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> UserCreate(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email =
                    userViewModel.Email,
                    // Add the Address Info:
                    Address = userViewModel.Address,
                    City = userViewModel.City,
                    State = userViewModel.State,
                    PostalCode = userViewModel.PostalCode
                };

                // Add the Address Info:
                user.Address = userViewModel.Address;
                user.City = userViewModel.City;
                user.State = userViewModel.State;
                user.PostalCode = userViewModel.PostalCode;

                // Then create:
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.OrderBy(x => x.Name).ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }

                    //Save into User Extension table
                    using (var db = new MercuryBackofficeDbContext())
                    {
                        db.AspNetUsersExtension.Add(new AspNetUsersExtension()
                        {
                            UserId = user.Id,
                            LastLoginOn = null,
                            CreatedBy = User.Identity.Name.ToString(),
                            CreatedOn = DateTime.Now,
                            UpdatedBy = User.Identity.Name.ToString(),
                            UpdatedOn = DateTime.Now
                        });
                        await db.SaveChangesAsync();
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles.OrderBy(x => x.Name).ToList(), "Name", "Name");
                    return View();

                }
                return RedirectToAction(PageURL.url_Management_User_List);
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles.OrderBy(x => x.Name).ToList(), "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> UserEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                // Include the Addresss info:
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserEdit([Bind(Include = "Email, Id, Address, City, State, PostalCode")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.Address = editUser.Address;
                user.City = editUser.City;
                user.State = editUser.State;
                user.PostalCode = editUser.PostalCode;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }


                //Update into User Extension table
                using (var db = new MercuryBackofficeDbContext())
                {
                    var userExt = await db.AspNetUsersExtension.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

                    if (userExt != null)
                    {
                        userExt.UpdatedBy = User.Identity.Name.ToString();
                        userExt.UpdatedOn = DateTime.Now;
                    }
                    await db.SaveChangesAsync();
                }

                return RedirectToAction(PageURL.url_Management_User_List);
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> UserDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserDeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                //Delete from User Extension table
                using (var db = new MercuryBackofficeDbContext())
                {
                    var userExt = await db.AspNetUsersExtension.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

                    if (userExt != null)
                    {
                        db.AspNetUsersExtension.Remove(userExt);
                    }
                    await db.SaveChangesAsync();
                }

                return RedirectToAction(PageURL.url_Management_User_List);
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetUserGrid(int? page, int? limit, string sortBy = null, string direction = "ASC", string searchString = null)
        //public JsonResult GetUserRoleGrid()
        {
            int total;
            var records = UserManager.Users.ToList();

            if (searchString != null)
            {
                searchString = searchString.ToLower();

                //Description might be null
                //records = records.Where(x => searchString.Any(txt => x.Name.ToLower().Contains(txt)) && searchString.Any(txt => x.Description.ToLower().Contains(txt))).ToList();

                //MUST SET TO LOWER CASE ONLY CAN SEARCH!!!!!
                records = records.Where(x => x.UserName.ToLower().Contains(searchString) || x.DisplayAddress.ToLower().Contains(searchString)).ToList();
            }

            //Set the columns to select
            //records = records.Select(x => new { x.Id, x.Name, x.Description }).ToList();

            //Default sorting
            if (sortBy == null) { sortBy = "UserName"; }

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


        #endregion
    }
}
