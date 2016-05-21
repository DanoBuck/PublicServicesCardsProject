using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublicServicesCardsProject.Models;
using Microsoft.AspNet.Identity;

namespace PublicServicesCardsProject.Controllers
{
    [Authorize(Roles = "Manager ,Customer, Staff")]
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();

            var appointments = from d in db.Appointments
                               select d;

            if (!String.IsNullOrEmpty(id) && (User.IsInRole("Manager") || User.IsInRole("Staff")))
            {
                var query = from d in db.Appointments
                            from u in db.Users
                            where d.StaffId == u.StaffId
                            where u.Id == id
                            select d;

                appointments = query;
            } 
            else if (!String.IsNullOrEmpty(id) && User.IsInRole("Customer")){
                var query = from d in db.Appointments
                            from u in db.Users
                            where d.CustomerId == u.CustomerId
                            where u.Id == id
                            select d;

                appointments = query;
            }


            return View(appointments.ToList().OrderBy(s => s.DateOfAppointment));
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
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice");
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId == id), "StaffId", "Name");
            ViewBag.User = new SelectList(db.Customers.Where(l => l.CustomerId == currentUser.CustomerId), "CustomerId", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            try {
                if (ModelState.IsValid)
                {
                    if (CheckAvailabityOfTimeAndDateIsNotAlreadyBooked(appointment) && CheckDateIsCorrect(appointment.DateOfAppointment))
                    {
                        appointment.CustomerId = currentUser.CustomerId.Value;
                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (!CheckDateIsCorrect(appointment.DateOfAppointment))
                    {
                        TempData["Error"] = "Appointments cannot be booked for dates before " + DateTime.Today.ToShortDateString();
                    }
                    else
                    {
                        TempData["Error"] = "No appointment available at " + appointment.TimeOfAppointment.ToShortTimeString() + " on " + appointment.DateOfAppointment.Date.ToShortDateString() 
                                            + " in this location";
                    }
                }
            } catch(DataException)
            {
                ModelState.AddModelError("Error Saving Data" , "");
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId.Equals(appointment.BuildingId)), "StaffId", "Name", appointment.StaffId);
            ViewBag.User = new SelectList(db.Customers.Where(l => l.CustomerId == currentUser.CustomerId), "CustomerId", "Name", appointment.CustomerId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Customer")]
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
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId == appointment.BuildingId), "StaffId", "Name", appointment.BuildingId);
            ViewBag.User = new SelectList(db.Customers.Where(l => l.CustomerId == currentUser.CustomerId), "CustomerId", "Name");
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                if (CheckAvailabityOfTimeAndDateIsNotAlreadyBooked(appointment) && CheckDateIsCorrect(appointment.DateOfAppointment))
                {
                    try {
                        appointment.CustomerId = currentUser.CustomerId.Value;
                        db.Entry(appointment).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    } catch (Exception)
                    {
                        ModelState.AddModelError("Error editing data", " ");
                    }
                }
                else if (!CheckDateIsCorrect(appointment.DateOfAppointment))
                {
                    TempData["Error"] = "Appointments cannot be booked for dates before " + DateTime.Today.ToShortDateString();
                }
                else
                {
                    TempData["Error"] = "No appointment available at " + appointment.TimeOfAppointment.ToShortTimeString() + " on " + appointment.DateOfAppointment.Date.ToShortDateString()
                                        + " in this location";
                }
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "SafeOffice", appointment.BuildingId);
            ViewBag.StaffId = new SelectList(db.Staff.Where(s => s.BuildingId == appointment.BuildingId), "StaffId", "Name", appointment.BuildingId);
            ViewBag.User = new SelectList(db.Customers.Where(l => l.CustomerId == currentUser.CustomerId), "CustomerId", "Name");
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
            try {
                db.Appointments.Remove(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error deleting data", "");
            }
            return View(appointment);
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
        * On 5/5/2016                                      
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

        public bool CheckAvailabityOfTimeAndDateIsNotAlreadyBooked(Appointment appointment)
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

        // This Method Stops Customers Booking Dates Already Past
        public bool CheckDateIsCorrect(DateTime appointmentDate)
        {
            bool isDateCorrect = true;
            var result = appointmentDate.Subtract(DateTime.Today);

            if(result.TotalDays >= 0)
            {
                isDateCorrect = true;
            }
            else if (result.TotalDays < 0) {
                isDateCorrect = false;
            }
            return isDateCorrect;
        }
    }
}
