using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;
using System.Text.RegularExpressions;

namespace Bonyan.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Gender).Where(u=>u.IsDeleted==false).OrderByDescending(u=>u.CreationDate).Include(u => u.Role).Where(u=>u.IsDeleted==false).OrderByDescending(u=>u.CreationDate);
            return View(users.ToList());
        }

        [Route("userprofile")]
        public ActionResult Details()
        { 
            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }
            string cellNum = User.Identity.Name;
           
            if (cellNum == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(current => current.IsActive && !current.IsDeleted && current.CellNum == cellNum).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            UserProfileViewModel userProfile = new UserProfileViewModel()
            {
                User = user,
                UserProductsLikes = db.UserProductsLikes.Where(current=>current.IsActive && !current.IsDeleted && current.UserId == user.Id).ToList()
            };
            return View(userProfile);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CellNum,Password,FullName,Email,Code,BirthDate,RoleId,GenderId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] User user)
        {
            if (ModelState.IsValid)
            {
				user.IsDeleted=false;
				user.CreationDate= DateTime.Now; 
                user.Id = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", user.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", user.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CellNum,Password,FullName,Email,Code,BirthDate,RoleId,GenderId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] User user)
        {
            if (ModelState.IsValid)
            {
				user.IsDeleted=false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", user.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Title", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
			user.IsDeleted=true;
			user.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult EditProfile(string fulname,string email,string celnum,string password,string id)
        {
            try
            {
                Guid userId = new Guid(id);
                User user = db.Users.Find(userId);
                if (!string.IsNullOrEmpty(fulname))
                {
                    user.FullName = fulname;
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                    if (!isEmail)
                        return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
                    user.Email = email;
                }
                else if (!string.IsNullOrEmpty(celnum))
                {
                    user.CellNum = celnum;
                }
                else if (!string.IsNullOrEmpty(password))
                {
                    user.Password = password;
                }

                user.IsDeleted = false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("false", JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}
