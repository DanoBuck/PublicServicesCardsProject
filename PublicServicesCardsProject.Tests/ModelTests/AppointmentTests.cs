using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Tests.ModelTests
{
    [TestClass]
    public class AppointmentTests
    {
        private Appointment appointment;

        [TestMethod]
        public void TestAppointmentsModel()
        {
            appointment = new Appointment
            {
                AppointmentId = 1,
                BuildingId = 1,
                StaffId = 1,
                CustomerId = 1,
                DateOfAppointment = DateTime.Today,
                TimeOfAppointment = "9:00",
                Customer = new Customer(),
                Staff = new Staff(),
                Building = new Building()
            };

            appointment.Should().NotBeNull();
        }
    }
}
