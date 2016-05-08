using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            return View(db.Buildings.ToList());
        }
    }
}