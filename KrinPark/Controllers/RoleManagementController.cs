using KrinPark.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrinPark.Controllers
{
    [Authorize]
    public class RoleManagementController : Controller
    {
        public RoleManagementController()
        {

        }
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        // GET: RoleManagement

        public RoleManagementController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
           
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            List<IdentityRoleViewModel> identityRoleViewModels = new List<IdentityRoleViewModel>();

           var roles = db.Roles.ToList();
            foreach (var item in roles)
            {
                IdentityRoleViewModel identityole = new IdentityRoleViewModel();
                identityole.RoleName = item.Name;
                identityRoleViewModels.Add(identityole);
            }
            return View(identityRoleViewModels);
            
        }

        public ActionResult CreateRole( )
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(IdentityRoleViewModel role)
        {

            var newRole = new IdentityRole();
            newRole.Name = role.RoleName;
            db.Roles.Add(newRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AssignRole() 
        {
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult AssignRoles(string id,string role)
        {

            var user = UserManager.FindById(id);
            UserManager.AddToRole(user.Id, role);
            db.SaveChanges();

            return RedirectToAction("AssignRole");
        }

    }
}