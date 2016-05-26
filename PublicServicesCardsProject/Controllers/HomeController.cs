using PublicServicesCardsProject.Models;
using System.Linq;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Quick Links";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            return View(db.Buildings.ToList());
        }

        public ActionResult PostalApplication()
        {
            return View();
        }

        // Code Based on http://www.codeproject.com/Tips/818197/Generate-PDF-in-ASP-NET-MVC-Using-Rotativa
        // Accessed on 24/5/2016
        [AllowAnonymous]
        public ActionResult GeneratePostalRegistrationPDF()
        {
            var model = new Customer();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"The Department of Social Protection\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.PartialViewAsPdf("PostalApplication", model)
            {
                FileName = "PostalApplication.pdf",
                CustomSwitches = footer
            };
        }
    }
}