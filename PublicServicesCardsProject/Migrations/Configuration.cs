namespace PublicServicesCardsProject.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PublicServicesCardsProject.DataAccess.PSCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PublicServicesCardsProject.DataAccess.PSCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var Staff = new List<Staff>()
            {
               new Staff
               {
                   FirstName = "Daniel",
                   LastName = "Buckley",
                   DateOfBirth = new DateTime(1995,1,9),
                   EmailAddress = "DaniielBuckleyTy3@gmail.com",
                   PPSN = "1234567Q",
                   Salary = 45000,
                   DeskNumber = 1,
                   Building = new Building
                   {
                       BuildingId = 1
                       // Staff Needs To Be Populated
                   },
                   Appointments = new List<Appointment>
                   {
                       new Appointment
                       {
                           AppointmentId = 1,
                           DateOfAppointment = DateTime.Today.Date,
                           TimeOfAppointment = DateTime.Now.AddHours(2),
                           Customer = new Customer
                           {
                               FirstName = "Adam",
                               LastName = "Buckley",
                               DateOfBirth = new DateTime(1996,12,2),
                               EmailAddress = "Adam@gmail.com",
                               PPSN = "1234567S",
                               CivilStatus = "Single"
                           },
                           Building = new Building
                           {
                               BuildingId = 1
                               // Staff Needs To Be Populated
                           }
                       }
                   }
               }
            };
        }
    }
}
