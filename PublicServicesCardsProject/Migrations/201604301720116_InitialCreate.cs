namespace PublicServicesCardsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        DateOfAppointment = c.DateTime(nullable: false),
                        TimeOfAppointment = c.DateTime(nullable: false),
                        Staff_PersonId = c.Int(),
                        Building_BuildingId = c.Int(),
                        Customer_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Person", t => t.Staff_PersonId)
                .ForeignKey("dbo.Building", t => t.Building_BuildingId)
                .ForeignKey("dbo.Person", t => t.Customer_PersonId)
                .Index(t => t.Staff_PersonId)
                .Index(t => t.Building_BuildingId)
                .Index(t => t.Customer_PersonId);
            
            CreateTable(
                "dbo.Building",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        SafeOffice = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        AddressLine4 = c.String(),
                        County = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        PPSN = c.String(),
                        Salary = c.Double(),
                        DeskNumber = c.Int(),
                        CivilStatus = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Building_BuildingId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Building", t => t.Building_BuildingId)
                .Index(t => t.Building_BuildingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointment", "Customer_PersonId", "dbo.Person");
            DropForeignKey("dbo.Appointment", "Building_BuildingId", "dbo.Building");
            DropForeignKey("dbo.Person", "Building_BuildingId", "dbo.Building");
            DropForeignKey("dbo.Appointment", "Staff_PersonId", "dbo.Person");
            DropIndex("dbo.Person", new[] { "Building_BuildingId" });
            DropIndex("dbo.Appointment", new[] { "Customer_PersonId" });
            DropIndex("dbo.Appointment", new[] { "Building_BuildingId" });
            DropIndex("dbo.Appointment", new[] { "Staff_PersonId" });
            DropTable("dbo.Person");
            DropTable("dbo.Building");
            DropTable("dbo.Appointment");
        }
    }
}
