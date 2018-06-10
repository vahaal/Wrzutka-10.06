namespace Wrzutka.Migrations.Wrzutka
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "UserID", c => c.Int(nullable: false));
        }
    }
}
