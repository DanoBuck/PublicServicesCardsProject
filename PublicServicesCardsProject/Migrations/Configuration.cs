namespace PublicServicesCardsProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
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
                   PPSN = "1234567A",
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
                   PPSN = "1234567B",
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
                   PPSN = "1234567C",
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
                   EmailAddress = "ThreasaOReilly@live.ie",
                   PPSN = "1234567D",
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
                   PPSN = "1234567E",
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
                   EmailAddress = "BillyOByrne@live.ie",
                   PPSN = "1234567F",
                   Salary = 49000,
                   DeskNumber = 2,
                   BuildingId = 3 // Swords
               },
            };
            staff.ForEach(a => context.Staff.AddOrUpdate(a));
            context.SaveChanges();

            CreateManagerRoleAndPopuate(context); // Makes Daniel Buckley the Manager
            CreateStaffRoleAndPopulate(context); // Makes All Staff Staff Members Excluding Daniel Buckley

            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Adam",
                    LastName = "Buckley",
                    DateOfBirth = new DateTime(1996, 12, 2),
                    EmailAddress = "Adam@gmail.com",
                    PPSN = "1234567G",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Amanda",
                    LastName = "Chapington",
                    DateOfBirth = new DateTime(1987,5,12),
                    EmailAddress = "Amanda@gmail.com",
                    PPSN = "1234567H",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 3,
                    FirstName = "Phil",
                    LastName = "Philipson",
                    DateOfBirth = new DateTime(1916, 3, 30),
                    EmailAddress = "Phil@gmail.com",
                    PPSN = "1234567I",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 4,
                    FirstName = "Imelda",
                    LastName = "Donaldson",
                    DateOfBirth = new DateTime(1982,2,10),
                    EmailAddress = "ImeldaDonaldson@gmail.com",
                    PPSN = "1234567J",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 5,
                    FirstName = "Finbarr",
                    LastName = "Furey",
                    DateOfBirth = new DateTime(1920, 3, 30),
                    EmailAddress = "FinbarrFurey@gmail.com",
                    PPSN = "1234567K",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 6,
                    FirstName = "Dog",
                    LastName = "Dogson",
                    DateOfBirth = new DateTime(1973,1,15),
                    EmailAddress = "Dogson@gmail.com",
                    PPSN = "1234567L",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 7,
                    FirstName = "Ellamae",
                    LastName = "Dahl",
                    DateOfBirth = new DateTime(1972,1,15),
                    EmailAddress = "EllamaeDahl@gmail.ie",
                    PPSN = "1234567M",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 8,
                    FirstName = "Nelle",
                    LastName = "Pace",
                    DateOfBirth = new DateTime(1977,1,15),
                    EmailAddress = "NellePace@gmail.ie",
                    PPSN = "1234567N",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 9,
                    FirstName = "Lawrence",
                    LastName = "Hunter",
                    DateOfBirth = new DateTime(1995,1,15),
                    EmailAddress = "LawrenceHunter@gmail.ie",
                    PPSN = "1234567O",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 10,
                    FirstName = "Scotty",
                    LastName = "Coldren",
                    DateOfBirth = new DateTime(1991,1,15),
                    EmailAddress = "ScottyColdren@gmail.ie",
                    PPSN = "1234567Z",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 11,
                    FirstName = "Pamela",
                    LastName = "O'Donaghue",
                    DateOfBirth = new DateTime(1995,1,15),
                    EmailAddress = "PamelaODonaghue@gmail.ie",
                    PPSN = "1234567P",
                    CivilStatus = "Married"
                },
                new Customer
                {
                    CustomerId = 12,
                    FirstName = "Tiffanie",
                    LastName = "Romain",
                    DateOfBirth = new DateTime(1995,1,12),
                    EmailAddress = "TiffanieRomain@gmail.ie",
                    PPSN = "1234567Q",
                    CivilStatus = "Single"
                },
                new Customer
                {
                    CustomerId = 13,
                    FirstName = "Wilfred",
                    LastName = "Brayboy",
                    DateOfBirth = new DateTime(1993,1,12),
                    EmailAddress = "WilfredBrayboy@gmail.ie",
                    PPSN = "1234567R",
                    CivilStatus = "Single"
                }
            };
            customers.ForEach(c => context.Customers.AddOrUpdate(c));
            context.SaveChanges();

            CreateCustomerRoleAndPopulate(context);

            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel Buckley
                    CustomerId = 3, // Adam Buckley,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "12:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel
                    CustomerId = 7, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "11:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel
                    CustomerId = 8, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "09:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel
                    CustomerId = 8, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "13:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Daniel
                    CustomerId = 9, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "14:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel
                    CustomerId = 10, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "15:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 1, // Daniel
                    CustomerId = 11, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "16:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Cecil
                    CustomerId = 4, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = "10:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Cecil
                    CustomerId = 12, // Amanda Chapington,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "09:00"
                },
                new Appointment
                {
                    BuildingId = 1, // Gandon
                    StaffId = 2, // Cecil
                    CustomerId = 13,
                    DateOfAppointment = DateTime.Today.AddMonths(1),
                    TimeOfAppointment = "14:00"
                },
                new Appointment
                {
                    BuildingId = 2, // Tallaght
                    StaffId = 3, // Freda Burns
                    CustomerId = 3, // Phil Philipson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = "11:00"
                },
                new Appointment
                {
                    BuildingId = 2, // Tallaght
                    StaffId = 4, // Threase O'Reilly
                    CustomerId = 4, // Imelda Donaldson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = "12:00"
                },
                new Appointment
                {
                    BuildingId = 3, // Swords
                    StaffId = 5, // MillyMasterson
                    CustomerId = 5, // Finbarr Furey
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = "13:00"
                },
                new Appointment
                {
                    BuildingId = 3, // Swords
                    StaffId = 6, // Billy O'Byrne
                    CustomerId = 6, // Dog Dogson
                    DateOfAppointment = DateTime.Today.Date,
                    TimeOfAppointment = "14:00"
                },
            };
            appointments.ForEach(a => context.Appointments.AddOrUpdate(a));
            context.SaveChanges();
        }

        public void CreateManagerRoleAndPopuate(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            const string ManagerRole = "Manager";

            var userRole = new IdentityRole { Name = ManagerRole, Id = Guid.NewGuid().ToString() };
            context.Roles.Add(userRole);

            PasswordHasher pass = new PasswordHasher();
            foreach(var staff in context.Staff)
            {
                if (staff.Name == "Daniel Buckley")
                {
                    var user = new ApplicationUser
                    {
                        StaffId = staff.StaffId,
                        UserName = staff.EmailAddress,
                        Email = staff.EmailAddress,
                        PasswordHash = pass.HashPassword("PassWord1'"),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
                    context.Users.Add(user);
                    base.Seed(context);
                }
            }
        }

        public void CreateStaffRoleAndPopulate(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            const string StaffRole = "Staff";

            var userRole = new IdentityRole { Name = StaffRole, Id = Guid.NewGuid().ToString() };
            context.Roles.Add(userRole);

            PasswordHasher pass = new PasswordHasher();
            foreach (var staff in context.Staff)
            {
                if (staff.Name != "Daniel Buckley")
                {
                    var user = new ApplicationUser
                    {
                        StaffId = staff.StaffId,
                        UserName = staff.EmailAddress,
                        Email = staff.EmailAddress,
                        PasswordHash = pass.HashPassword("PassWord1'"),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
                    context.Users.Add(user);
                    base.Seed(context);
                }
            }
        }

        public void CreateCustomerRoleAndPopulate(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            const string CustomerRole = "Customer";

            var userRole = new IdentityRole { Name = CustomerRole, Id = Guid.NewGuid().ToString() };
            context.Roles.Add(userRole);

            PasswordHasher pass = new PasswordHasher();
            foreach (var customer in context.Customers)
            {
                var user = new ApplicationUser
                {
                    CustomerId = customer.CustomerId,
                    UserName = customer.EmailAddress,
                    Email = customer.EmailAddress,
                    PasswordHash = pass.HashPassword("PassWord1'"),
                    SecurityStamp = Guid.NewGuid().ToString()      
                };
                user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
                context.Users.Add(user);
                base.Seed(context);
            }
        }
    }
}
