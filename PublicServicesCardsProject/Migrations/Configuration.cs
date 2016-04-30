namespace PublicServicesCardsProject.Migrations
{
    using DataAccess;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PSCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PSCContext context)
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
            var staff = new List<Staff>()
            {
               new Staff
               {
                   PersonId = 1,
                   FirstName = "Daniel",
                   LastName = "Buckley",
                   DateOfBirth = new DateTime(1995,1,9),
                   EmailAddress = "DaniielBuckleyTy3@gmail.com",
                   PPSN = "1234567Q",
                   Salary = 45000,
                   DeskNumber = 1,
               },
               new Staff
               {
                   PersonId = 2,
                   FirstName = "Cecil",
                   LastName = "O'Hegarthy",
                   DateOfBirth = new DateTime(1990,6,12),
                   EmailAddress = "CecilOHegarthy@gmail.com",
                   PPSN = "1234567A",
                   Salary = 49000,
                   DeskNumber = 2,
               }
            };
            staff.ForEach(a => context.Staff.AddOrUpdate(a));
            context.SaveChanges();

            var buildings = new List<Building>
            {
                new Building
                {
                    SafeOffice = "Gandon PSC Centre",
                    AddressLine1 = "PSC Gandon",
                    AddressLine2 = "Public Services Card Centre",
                    AddressLine3 = "Gandon House",
                    AddressLine4 = "Amiens Street ",
                    County = "Dublin",
                    Phone = "(01) 8172640"
                }
            };
            buildings.ForEach(b => context.Buildings.AddOrUpdate(b));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer
                {
                    PersonId = 3,
                    FirstName = "Adam",
                    LastName = "Buckley",
                    DateOfBirth = new DateTime(1996, 12, 2),
                    EmailAddress = "Adam@gmail.com",
                    PPSN = "1234567S",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    PersonId = 4,
                    FirstName = "Amanda",
                    LastName = "Chapington",
                    DateOfBirth = new DateTime(1987,5,12),
                    EmailAddress = "Adam@gmail.com",
                    PPSN = "1234567A",
                    CivilStatus = "Married"
                }
            };
            customers.ForEach(c => context.Customers.AddOrUpdate(c));
            context.SaveChanges();

            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel Buckley
                    CustomerId = 3, // Adam Buckley,
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddHours(2)
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Cecil
                    CustomerId = 4, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddHours(4)
                }
            };
        }
    }
}
