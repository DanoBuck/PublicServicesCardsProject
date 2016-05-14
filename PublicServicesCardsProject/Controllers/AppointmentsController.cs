using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublicServicesCardsProject.Models;

namespace PublicServicesCardsProject.Controllers
{
    [Authorize(Roles = "Customer, Staff")]
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Building).Include(a => a.Staff);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create(int? id)
        {
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId == id), "StaffId", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (CheckAvailabityOfTimeAndDate(appointment))
                {
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string message = "No appointment available at" + appointment.TimeOfAppointment + " on " + appointment.DateOfAppointment
                        + " with " + appointment.StaffId + " in " + appointment.BuildingId;
                }
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId.Equals(appointment.BuildingId)), "StaffId", "Name", appointment.StaffId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId == appointment.BuildingId), "StaffId", "Name", appointment.BuildingId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            //ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId.Equals(appointment.BuildingId)), "StaffId", "Name", appointment.BuildingId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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

        /*
        * This code is based on that of found at http://www.devcurry.com/2013/01aspnet-mvc-cascading-dropdown-list.html
        * On 5/5/2016 underneath                                      
        */
        public JsonResult GetAllStaffInBuilding(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = from d in db.Staff
                        where d.BuildingId == id
                        select d;

            IEnumerable<Staff> staffs = query;
            return Json(staffs);
        }

        public bool CheckAvailabityOfTimeAndDate(Appointment appointment)
        {
            var query = from d in db.Appointments
                        select d;

            query = query.Where(x => x.TimeOfAppointment == appointment.TimeOfAppointment)
                    .Where(x => x.DateOfAppointment == appointment.DateOfAppointment)
                    .Where(x => x.StaffId == appointment.StaffId);

            bool returnValue = true;

            if(query.Count() == 0)
            {
                returnValue = true; // Appointment is Available
            }
            else
            {
                returnValue = false; // Appointment is not Available
            }
            return returnValue;
        }
    }
}
