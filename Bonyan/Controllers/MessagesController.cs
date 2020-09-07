using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace Bonyan.Controllers
{
    public class MessagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.MessageStatus).Where(m => m.IsDeleted == false).OrderByDescending(m => m.CreationDate).Include(m => m.User).Where(m => m.IsDeleted == false).OrderByDescending(m => m.CreationDate);
            return View(messages.ToList());
        }

    
        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.MessageStatusId = new SelectList(db.MessageStatuses, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,Body,AttachmentFileUrl,Response,UserId,MessageStatusId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.IsDeleted = false;
                message.CreationDate = DateTime.Now;
                message.Id = Guid.NewGuid();
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MessageStatusId = new SelectList(db.MessageStatuses, "Id", "Title", message.MessageStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum", message.UserId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.MessageStatusId = new SelectList(db.MessageStatuses, "Id", "Title", message.MessageStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum", message.UserId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Body,AttachmentFileUrl,Response,UserId,MessageStatusId,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.IsDeleted = false;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MessageStatusId = new SelectList(db.MessageStatuses, "Id", "Title", message.MessageStatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "CellNum", message.UserId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Message message = db.Messages.Find(id);
            message.IsDeleted = true;
            message.DeletionDate = DateTime.Now;

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

        [Route("message")]
        [Authorize(Roles = "customer")]
        public ActionResult UserCreate()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop", "Home");
            }
            return View();
        }

        [Route("message")]
        [HttpPost]
        [Authorize(Roles = "customer")]
        public ActionResult UserCreate(ViewModels.MessageViewModel message,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                Message oMessage=new Message();
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/message/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileUpload.SaveAs(physicalFilename);
                    oMessage.AttachmentFileUrl = newFilenameUrl;
                }


                if (!User.Identity.IsAuthenticated)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string cellNum = User.Identity.Name;

                if (cellNum == null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                User user = db.Users.Where(current => current.IsActive && !current.IsDeleted && current.CellNum == cellNum).FirstOrDefault();
                if (user == null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }


                oMessage.Subject = message.Subject;
                oMessage.Body = message.Body;
                oMessage.MessageStatusId = db.MessageStatuses.FirstOrDefault(c => c.Order == 1).Id;
                oMessage.UserId = user.Id;
                oMessage.CreationDate=DateTime.Now;
                oMessage.Id = Guid.NewGuid();
                oMessage.IsDeleted = false;
                oMessage.IsActive = true;

                db.Messages.Add(oMessage);
                db.SaveChanges();

                ViewBag.successMessage = "submit successfully";
                return View();

                #endregion
            }

            return View();
        }


        [Route("message/list")]
        [Authorize(Roles = "customer")]
        public ActionResult List()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop", "Home");
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string cellNum = User.Identity.Name;

            if (cellNum == null)
            {
                return RedirectToAction("Login", "Account");
            }

            User user = db.Users.FirstOrDefault(current =>
                current.IsActive && !current.IsDeleted && current.CellNum == cellNum);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Message> messages = db.Messages.Where(c => c.UserId == user.Id && c.IsDeleted == false).OrderByDescending(c=>c.CreationDate).ToList();

             MessageListViewModel  messageList=new MessageListViewModel();

            messageList.Messages = messages;

            return View(messageList);
        }

        [Route("message/detail/{id:Guid}")]
        [Authorize(Roles = "customer")]
        public ActionResult Details(Guid id)
        {

            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            MessageDetailViewModel messageDetail = new MessageDetailViewModel()
            {
                Message = message
            };
            return View(messageDetail);
        }

    }
}
