using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Bonyan.Controllers
{
    public class ArtistsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Artists.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
				artist.IsDeleted=false;
				artist.CreationDate= DateTime.Now; 
                artist.Id = Guid.NewGuid();
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
				artist.IsDeleted=false;
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Artist artist = db.Artists.Find(id);
			artist.IsDeleted=true;
			artist.DeletionDate=DateTime.Now;
 
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
