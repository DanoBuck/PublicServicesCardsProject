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
            var buildings = new List<Building>
            {
                // 1
                new Building
                {
                    SafeOffice = "Gandon PSC Centre",
                    AddressLine1 = "PSC Gandon",
                    AddressLine2 = "Public Services Card Centre",
                    AddressLine3 = "Gandon House",
                    AddressLine4 = "Amiens Street ",
                    County = "Dublin",
                    Phone = "(01) 8172640"
                },
                // 2
                new Building
                {
                    SafeOffice = "Tallaght PSC Centre",
                    AddressLine1 = "Unit 247",
                    AddressLine2 = "Level 2",
                    AddressLine3 = "The Square Shopping Centre",
                    AddressLine4 = "Tallaght",
                    County = "Dublin",
                    Phone = "(01) 4629496"
                },
                // 3
                new Building
                {
                    SafeOffice = "Intreo Centre Swords",
                    AddressLine1 = "Mainscourt",
                    AddressLine2 = "23 Main Street",
                    AddressLine3 = "Swords",
                    //AddressLine4 = "Tallaght",
                    County = "Dublin",
                    Phone = "(01) 4629496"
                }
            };
            buildings.ForEach(b => context.Buildings.AddOrUpdate(b));
            context.SaveChanges();

            var staff = new List<Staff>
            {
               new Staff
               {
                   StaffId = 1,
                   FirstName = "Daniel",
                   LastName = "Buckley",
                   DateOfBirth = new DateTime(1995,1,9),
                   EmailAddress = "DaniielBuckleyTy3@gmail.com",
                   PPSN = "1234567Q",
                   Salary = 45000,
                   DeskNumber = 1,
                   BuildingId = 1 // Gandon
               },
               new Staff
               {
                   StaffId = 2,
                   FirstName = "Cecil",
                   LastName = "O'Hegarthy",
                   DateOfBirth = new DateTime(1990,6,12),
                   EmailAddress = "CecilOHegarthy@gmail.com",
                   PPSN = "1234567A",
                   Salary = 49000,
                   DeskNumber = 2,
                   BuildingId = 1 // Gandon
               },
               new Staff
               {
                   StaffId = 3,
                   FirstName = "Freda",
                   LastName = "Burns",
                   DateOfBirth = new DateTime(1965,5,1),
                   EmailAddress = "FredaBurns@outlook.com",
                   PPSN = "1234567B",
                   Salary = 45000,
                   DeskNumber = 1,
                   BuildingId = 2 // Tallaght PSC
               },
               new Staff
               {
                   StaffId = 4,
                   FirstName = "Threasa",
                   LastName = "O'Reilly",
                   DateOfBirth = new DateTime(1980,12,6),
                   EmailAddress = "ThreasaO'Reilly@live.ie",
                   PPSN = "1234567C",
                   Salary = 49000,
                   DeskNumber = 2,
                   BuildingId = 2 // Tallaght PSC
               },
               new Staff
               {
                   StaffId = 5,
                   FirstName = "Milly",
                   LastName = "Masterson",
                   DateOfBirth = new DateTime(1955,5,1),
                   EmailAddress = "MillyMasterson@outlook.com",
                   PPSN = "123456KA",
                   Salary = 45000,
                   DeskNumber = 1,
                   BuildingId = 3 // Swords
               },
               new Staff
               {
                   StaffId = 6,
                   FirstName = "Billy",
                   LastName = "O'Byrne",
                   DateOfBirth = new DateTime(1979,12,6),
                   EmailAddress = "BillyO'Byrne@live.ie",
                   PPSN = "123456BA",
                   Salary = 49000,
                   DeskNumber = 2,
                   BuildingId = 3 // Swords
               },
            };
            staff.ForEach(a => context.Staff.AddOrUpdate(a));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Adam",
                    LastName = "Buckley",
                    DateOfBirth = new DateTime(1996, 12, 2),
                    EmailAddress = "Adam@gmail.com",
                    PPSN = "1234567S",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Amanda",
                    LastName = "Chapington",
                    DateOfBirth = new DateTime(1987,5,12),
                    EmailAddress = "Adam@gmail.com",
                    PPSN = "1234567A",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 3,
                    FirstName = "Phil",
                    LastName = "Philipson",
                    DateOfBirth = new DateTime(1916, 3, 30),
                    EmailAddress = "Phil@gmail.com",
                    PPSN = "1234567S",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 4,
                    FirstName = "Imelda",
                    LastName = "Donaldson",
                    DateOfBirth = new DateTime(1982,2,10),
                    EmailAddress = "ImeldaDonaldson@gmail.com",
                    PPSN = "1234567L",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 5,
                    FirstName = "Finbarr",
                    LastName = "Furey",
                    DateOfBirth = new DateTime(1920, 3, 30),
                    EmailAddress = "FinbarrFurey@gmail.com",
                    PPSN = "1234567H",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 6,
                    FirstName = "Dog",
                    LastName = "Dogson",
                    DateOfBirth = new DateTime(1973,1,15),
                    EmailAddress = "Dogson@gmail.com",
                    PPSN = "1234567Q",
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
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Cecil
                    CustomerId = 4, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
                new Appointment
                {
                    BuildingId = 2, // Tallaght
                    StaffId = 3, // Freda Burns
                    CustomerId = 3, // Phil Philipson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
                new Appointment
                {
                    BuildingId = 2, // Tallaght
                    StaffId = 4, // Threase O'Reilly
                    CustomerId = 4, // Imelda Donaldson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
                new Appointment
                {
                    BuildingId = 3, // Swords
                    StaffId = 5, // MillyMasterson
                    CustomerId = 5, // Finbarr Furey
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
                new Appointment
                {
                    BuildingId = 3, // Swords
                    StaffId = 6, // Billy O'Byrne
                    CustomerId = 6, // Dog Dogson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = DateTime.Now.AddMinutes(15)
                },
            };
            appointments.ForEach(a => context.Appointments.AddOrUpdate(a));
            context.SaveChanges();
        }
    }
}
