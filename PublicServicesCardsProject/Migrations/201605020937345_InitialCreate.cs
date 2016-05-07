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
                        BuildingId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DateOfAppointment = c.DateTime(nullable: false),
                        TimeOfAppointment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Building", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Staff", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.BuildingId)
                .Index(t => t.StaffId)
                .Index(t => t.CustomerId);
            
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
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        PPSN = c.String(),
                        CivilStatus = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        PPSN = c.String(),
                        Salary = c.Double(nullable: false),
                        DeskNumber = c.Int(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Building", t => t.BuildingId, cascadeDelete: false)
                .Index(t => t.BuildingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staff", "BuildingId", "dbo.Building");
            DropForeignKey("dbo.Appointment", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Appointment", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Appointment", "BuildingId", "dbo.Building");
            DropIndex("dbo.Staff", new[] { "BuildingId" });
            DropIndex("dbo.Appointment", new[] { "CustomerId" });
            DropIndex("dbo.Appointment", new[] { "StaffId" });
            DropIndex("dbo.Appointment", new[] { "BuildingId" });
            DropTable("dbo.Staff");
            DropTable("dbo.Customer");
            DropTable("dbo.Building");
            DropTable("dbo.Appointment");
        }
    }
}
