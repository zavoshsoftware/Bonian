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

namespace Bonyan.Controllers
{
    public class NotificationsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Notifications
        public ActionResult Index()
        {
            var notifications = db.Notifications.Include(n => n.User).Where(n=>n.IsDeleted==false).OrderByDescending(n=>n.CreationDate);
            return View(notifications.ToList());
        }

      
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificationCrudViewModel notificationCrud)
        {
            if (ModelState.IsValid)
            {
                if (notificationCrud.SendToAll)

                {
                    List<User> users = db.Users.Where(c => c.IsDeleted == false && c.IsActive).ToList();

                    foreach (User user in users)
                    {
                        Notification notification = new Notification();
                        notification.IsActive = true;
                        notification.IsDeleted = false;
                        notification.CreationDate = DateTime.Now;
                        notification.Id = Guid.NewGuid();

                        notification.Title = notificationCrud.Title;
                        notification.Message = notificationCrud.Message;
                        notification.Description = notificationCrud.Description;
                        notification.Seen = false;
                        notification.UserId = user.Id;

                        db.Notifications.Add(notification);
                    }
                }

                else
                {
                    if (notificationCrud.UserId == null)
                    {
                        ModelState.AddModelError("nulUserId","کاربر دریافت کننده این پیام را انتخاب نمایید.");
                        ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", notificationCrud.UserId);
                        return View(notificationCrud);
                    }
                    Notification notification = new Notification();
                    notification.IsActive = true;
                    notification.IsDeleted = false;
                    notification.CreationDate = DateTime.Now;
                    notification.Id = Guid.NewGuid();

                    notification.Title = notificationCrud.Title;
                    notification.Message = notificationCrud.Message;
                    notification.Description = notificationCrud.Description;
                    notification.Seen = false;
                    notification.UserId = notificationCrud.UserId;

                    db.Notifications.Add(notification);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", notificationCrud.UserId);
            return View(notificationCrud);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum", notification.UserId);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Message,UserId,Seen,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Notification notification)
        {
            if (ModelState.IsValid)
            {
				notification.IsDeleted=false;
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum", notification.UserId);
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Notification notification = db.Notifications.Find(id);
			notification.IsDeleted=true;
			notification.DeletionDate=DateTime.Now;
 
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
    }
}
