using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Bonyan.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        [Authorize(Roles = "customer")]
        [Route("")]
        public ActionResult Index()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            HomeViewModel home = new HomeViewModel()
            {
                Products = db.Products.Where(current => current.IsActive && !current.IsDeleted).OrderByDescending(current => current.Order).ToList()
            };
            return View(home);
        }
        public ActionResult IndexDesktop()
        {
            return View();
        }
        [Route("details")]
        public ActionResult Details()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
        }
        [Route("author")]
        public ActionResult Author()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
        }
        [Route("profile")]
        public ActionResult UserProfile()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
        }
        [Authorize(Roles = "customer")]
        [Route("search")]
        public ActionResult Search()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
        }
        [Route("like")]
        public ActionResult Like()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
        }
        [Route("about")]
        [Authorize(Roles = "customer")]
        public ActionResult About()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            AboutViewModel about = new AboutViewModel()
            {
                AboutTexts = db.Texts.Where(current =>
                    current.TextType.Name == "abouttexts" && current.IsActive && !current.IsDeleted).ToList(),

                Blogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c => c.Order).ToList()
            };
            return View(about);
        }


        [AllowAnonymous]
        public ActionResult GetSearchItem(string searchValue)
        {
            List<SearchViewModel> searchItems = new List<SearchViewModel>();

            List<Product> products = db.Products.Include(p => p.Artist).Where(c =>
                c.Artist.FullName.Contains(searchValue) || c.Artist.FullNameEn.Contains(searchValue)).ToList();

            foreach (Product product in products)
            {
                searchItems.Add(new SearchViewModel()
                {
                    Title = product.Artist.FullNameSrt,
                    ImageUrl = product.ImageUrl,
                    Url = "/product/" + product.Code
                });
            }
            return Json(searchItems, JsonRequestBehavior.AllowGet);
        }
    }
}