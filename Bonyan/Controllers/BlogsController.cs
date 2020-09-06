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
using ViewModels;

namespace Bonyan.Controllers
{
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Blogs.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }


        [Route("blog/{id:int}")]
        [Authorize(Roles = "customer")]
        public ActionResult Details(int id)
        {
            Blog blog = db.Blogs.FirstOrDefault(c => c.Code == id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            BlogDetailViewModel blogDetail=new BlogDetailViewModel()
            {
                Blog = blog
            };
            return View(blogDetail);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion

                blog.Code = CodeCreator.ReturnBlogCode();
                blog.IsDeleted=false;
				blog.CreationDate= DateTime.Now; 
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDeleted=false;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
			blog.IsDeleted=true;
			blog.DeletionDate=DateTime.Now;
 
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
