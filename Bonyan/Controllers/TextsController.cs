﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;

namespace Bonyan.Controllers
{
    public class TextsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Texts
        public ActionResult Index(Guid id)
        {
            var texts = db.Texts.Include(t => t.TextType).Where(t=>t.IsDeleted==false && t.TextTypeId==id).OrderByDescending(t=>t.CreationDate);
            return View(texts.ToList());
        }

        // GET: Texts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // GET: Texts/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Texts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Text text,Guid id,HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    text.ImageUrl = newFilenameUrl;
                }
                #endregion
                text.IsDeleted=false;
				text.CreationDate= DateTime.Now; 
                text.Id = Guid.NewGuid();
                text.TextTypeId = id;
                db.Texts.Add(text);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=id});
            }

            ViewBag.Id = id;
            return View(text);
        }

        // GET: Texts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = text.TextTypeId;
            return View(text);
        }

        // POST: Texts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Text text,HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    text.ImageUrl = newFilenameUrl;
                }
                #endregion
                text.IsDeleted=false;
                db.Entry(text).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new {id = text.TextTypeId });
            }
            ViewBag.Id = text.TextTypeId;
            return View(text);
        }

        // GET: Texts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = text.TextTypeId;
            return View(text);
        }

        // POST: Texts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Text text = db.Texts.Find(id);
			text.IsDeleted=true;
			text.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id = text.TextTypeId });
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
