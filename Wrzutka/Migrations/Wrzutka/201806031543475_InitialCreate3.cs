namespace Wrzutka.Migrations.Wrzutka
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        FileID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: true)
                .Index(t => t.FileID);
            
            AddColumn("dbo.Files", "Average", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "FileID", "dbo.Files");
            DropIndex("dbo.Ratings", new[] { "FileID" });
            DropColumn("dbo.Files", "Average");
            DropTable("dbo.Ratings");
        }
    }
}
