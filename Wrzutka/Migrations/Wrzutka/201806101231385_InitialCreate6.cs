namespace Wrzutka.Migrations.Wrzutka
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "FileName", c => c.String());
            DropColumn("dbo.Files", "CreationTime");
        }
    }
}
