using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshop.Helpers;
using Models;

namespace Bonyan.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Artist).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate).Include(p => p.ProductGroup).Where(p=>p.IsDeleted==false).OrderByDescending(p=>p.CreationDate);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FullName");
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    product.ImageUrl = newFilenameUrl;
                }
                #endregion

                product.LikeNumber = 0;
                product.Code = CodeCreator.ReturnUserCode();
				product.IsDeleted=false;
				product.CreationDate= DateTime.Now; 
                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FullName", product.ArtistId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FullName", product.ArtistId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/product/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                product.IsDeleted=false;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "Id", "FullName", product.ArtistId);
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
			product.IsDeleted=true;
			product.DeletionDate=DateTime.Now;
 
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
