namespace Wrzutka.Migrations.Wrzutka
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "AuthorUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "AuthorUserName");
        }
    }
}
