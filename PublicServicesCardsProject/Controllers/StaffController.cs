using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PublicServicesCardsProject.Models;

namespace PublicServicesCardsProject.Controllers
{
    [Authorize(Roles = "Manager")]
    public class StaffController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Staff
        public ActionResult Index(string office)
        {
            var staff = from s in db.Staff
                        select s;

            if (!String.IsNullOrEmpty(office)){
                staff = staff.Where(s => s.Building.SafeOffice.Equals(office));
            }
            ViewBag.office = new SelectList(db.Buildings, "SafeOffice", "SafeOffice");
            return View(staff.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,FirstName,LastName,DateOfBirth,EmailAddress,PPSN,Salary,DeskNumber,BuildingId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Staff.Add(staff);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error creating data", "");
                }
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", staff.BuildingId);
            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,FirstName,LastName,DateOfBirth,EmailAddress,PPSN,Salary,DeskNumber,BuildingId")] Staff staff)
        {
            if (ModelState.IsValid && CheckDeskNumber(staff.DeskNumber, staff.BuildingId))
            {
                try {
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 
                catch (Exception)
                {
                    ModelState.AddModelError("Error editing data", "");
                }
            }
            else
            {
                var building = db.Buildings.Where(x => x.BuildingId == staff.BuildingId).Select( x => x.SafeOffice).FirstOrDefault();
                TempData["Error"] = "Someone sits at this desk: " + staff.DeskNumber + " in: " + building;
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", staff.BuildingId);
            return View(staff);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staff.Find(id);
            var app = db.Users.Where(s => s.StaffId == id).FirstOrDefault();
            try
            {
                db.Users.Remove(app);
                db.Staff.Remove(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch (Exception)
            {
                TempData["Error"] = "Error Deleting Staff Member";
            }
            return View(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool CheckDeskNumber(int? deskNumber, int? buildingId)
        {
            bool returnValue = false;
            var query = from d in db.Staff
                        where d.DeskNumber == deskNumber
                        where d.BuildingId == buildingId
                        select d;

            if(query.Count() == 0)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
