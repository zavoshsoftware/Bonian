using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Bonyan.Controllers
{
    public class HomeController : Controller
    { DatabaseContext db = new DatabaseContext();
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            HomeViewModel home = new HomeViewModel()
            {
                Products=db.Products.Where(current=>current.IsActive && !current.IsDeleted).OrderByDescending(current => current.Order).ToList()
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
        public ActionResult About()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            AboutViewModel about = new AboutViewModel()
            {
                AboutTexts = db.Texts.Where(current => current.TextType.Name == "abouttexts" && current.IsActive && !current.IsDeleted).ToList()
            };
            return View(about);
        }
    }
}