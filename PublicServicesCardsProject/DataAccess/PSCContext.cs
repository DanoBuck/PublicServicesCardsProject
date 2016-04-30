using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.DataAccess
{
    class PSCContext : DbContext
    {
        // Same As The Connection String In Web Config
        public PSCContext() : base ("DefaultConnection")
        {   
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Appointment> Appontments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<PSCContext>(new DropCreateDatabaseIfModelChanges<PSCContext>());
        }
    }
}
