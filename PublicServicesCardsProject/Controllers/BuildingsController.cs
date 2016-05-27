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
    public class BuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buildings
        public ActionResult Index(string searchString)
        {
            var query = from d in db.Buildings
                        select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.SafeOffice.Contains(searchString));
                if(query.Count() == 0)
                {
                    TempData["Error"] = "No Match Found Of " + searchString;
                    return View(db.Buildings.ToList());
                }
            }
            return View(query.ToList());
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            building.Staff = FindStaffInBuilding(id);
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingId,SafeOffice,AddressLine1,AddressLine2,AddressLine3,AddressLine4,County,Phone")] Building building)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Buildings.Add(building);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (Exception)
                {
                    ModelState.AddModelError("Error creating data", "");
                }
            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingId,SafeOffice,AddressLine1,AddressLine2,AddressLine3,AddressLine4,County,Phone")] Building building)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Entry(building).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (Exception)
                {
                    ModelState.AddModelError("Error editing data", "");
                }
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            try
            {
                foreach(var staff in db.Staff.Where(x => x.BuildingId == id))
                {
                    staff.BuildingId = 2;
                }
                db.Buildings.Remove(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception d)
            {
                TempData["Error"] = "Cannot delete this building as all staff within the building will be deleted!" + d;
            }
            return View(building);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public IQueryable<Staff> FindStaffInBuilding(int? id)
        {
            var query = from d in db.Staff
                        where d.BuildingId == id
                        select d;

            var list = query;

            return list;
        }
    }
}
