using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonyan.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            if (!Request.Browser.IsMobileDevice)
            {
                return RedirectToAction("IndexDesktop");
            }
            return View();
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
            return View();
        }
    }
}