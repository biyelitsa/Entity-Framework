namespace OgrenciProjeCodeFirstt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        CityId = c.Int(nullable: false),
                        MotherName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Salary = c.Int(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        CityId = c.Int(nullable: false),
                        MotherName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Students", "CityId", "dbo.Cities");
            DropIndex("dbo.Teachers", new[] { "CityId" });
            DropIndex("dbo.Students", new[] { "CityId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Cities");
        }
    }
}
