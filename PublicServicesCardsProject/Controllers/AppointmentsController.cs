using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PublicServicesCardsProject.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace PublicServicesCardsProject.Controllers
{
    [Authorize(Roles = "Manager ,Customer, Staff")]
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index(string office)
        { 
            var id = User.Identity.GetUserId();

            var appointments = from d in db.Appointments
                               select d;
            if(User.IsInRole("Manager") && !String.IsNullOrEmpty(office))
            {
                appointments = appointments.Where(s => s.Building.SafeOffice.Equals(office));
                appointments = appointments.OrderBy(x => x.DateOfAppointment).ThenBy(x => x.TimeOfAppointment);
                ViewBag.office = new SelectList(db.Buildings, "SafeOffice", "SafeOffice");
                return View(appointments.ToList());
            }

            if (!String.IsNullOrEmpty(id) && (User.IsInRole("Manager") || User.IsInRole("Staff")))
            {
                var query = from d in db.Appointments
                            from u in db.Users
                            where d.StaffId == u.StaffId
                            where u.Id == id
                            select d;

                query = query.OrderBy(x => x.DateOfAppointment).ThenBy(x => x.TimeOfAppointment);
                appointments = query;
            } 
            else if (!String.IsNullOrEmpty(id) && User.IsInRole("Customer")){
                var query = from d in db.Appointments
                            from u in db.Users
                            where d.CustomerId == u.CustomerId
                            where u.Id == id
                            select d;

                query = query.OrderBy(x => x.DateOfAppointment).ThenBy(x => x.TimeOfAppointment);
                appointments = query;
            }

            ViewBag.office = new SelectList(db.Buildings, "SafeOffice", "SafeOffice");
            return View(appointments.ToList().OrderBy(x => x.DateOfAppointment).ThenBy(x => x.TimeOfAppointment));
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
        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Customer")]
        public ActionResult Create([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            try {
                if (ModelState.IsValid)
                {
                    if (CheckAvailabityOfTimeAndDateIsNotAlreadyBooked(appointment) && CheckDateIsCorrect(appointment.DateOfAppointment) && !CheckWeekends(appointment.DateOfAppointment))
                    {
                        appointment.CustomerId = currentUser.CustomerId.Value;
                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        SendEmailConfirmingAppointment(appointment);
                        return RedirectToAction("Index");
                    }
                    else if (!CheckDateIsCorrect(appointment.DateOfAppointment))
                    {
                        TempData["Error"] = "Appointments cannot be booked for dates before " + DateTime.Today.ToShortDateString();
                    }
                    else if (CheckWeekends(appointment.DateOfAppointment))
                    {
                        TempData["Error"] = "Appointments cannot be booked for Saturdays or Sundays!";
                    }
                    else
                    {
                        TempData["Error"] = "No appointment available at " + appointment.TimeOfAppointment+ " on " + appointment.DateOfAppointment.Date.ToShortDateString() 
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
        [Authorize(Roles = "Customer")]
        public ActionResult Edit([Bind(Include = "AppointmentId,BuildingId,StaffId,CustomerId,DateOfAppointment,TimeOfAppointment")] Appointment appointment)
        {
            var manager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                if (CheckAvailabityOfTimeAndDateIsNotAlreadyBooked(appointment) && CheckDateIsCorrect(appointment.DateOfAppointment) && !CheckWeekends(appointment.DateOfAppointment))
                {
                    try {
                        appointment.CustomerId = currentUser.CustomerId.Value;
                        db.Entry(appointment).State = EntityState.Modified;
                        db.SaveChanges();
                        SendEmailConfirmingAppointment(appointment);
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
                else if (CheckWeekends(appointment.DateOfAppointment))
                {
                    TempData["Error"] = "Appointments cannot be booked for Saturdays or Sundays!";
                }
                else
                {
                    TempData["Error"] = "No appointment available at " + appointment.TimeOfAppointment + " on " + appointment.DateOfAppointment.Date.ToShortDateString()
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

        public JsonResult GetAppointmentsDataJson()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = from d in db.Appointments
                        select d;

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public void SendEmailConfirmingAppointment(Appointment model)
        {
            MailMessage mail = new MailMessage();
            var getCustomerEmail = from d in db.Customers
                                   where d.CustomerId == model.CustomerId
                                   select d.EmailAddress;
            var getStaffName = from d in db.Staff
                                where d.StaffId == model.StaffId
                                select d.FirstName + " " + d.LastName;
            var getBuilding = from d in db.Buildings
                              where d.BuildingId == model.BuildingId
                              select d.SafeOffice;
            string email = getCustomerEmail.SingleOrDefault();
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Appointment Confirmation - Public Services Cards Online";
            mail.Body = string.Format("<h1>Appointment Made</h1> <hr>Date: <strong>" + model.DateOfAppointment.ToShortDateString() + "</strong><hr> Time: <strong>" + model.TimeOfAppointment + "</strong><hr> With:<strong> " + getStaffName.SingleOrDefault() + "</strong><hr> Location: <strong>" + getBuilding.SingleOrDefault() + "</strong>");
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception)
                {
                    MailMessage errorMail = new MailMessage();
                    errorMail.To.Add(new MailAddress("PublicServicesCardsOnline@gmail.com"));
                    errorMail.Subject = "Register Exception - Public Services Cards Online";
                    errorMail.Body = string.Format("Exception Has Been Thrown");
                    smtp.Send(errorMail);
                }
            }
        }

        public bool CheckWeekends(DateTime? date)
        {
            bool returnValue = false;
            if (date.Value.DayOfWeek.Equals(DayOfWeek.Saturday) || date.Value.DayOfWeek.Equals(DayOfWeek.Sunday))
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
