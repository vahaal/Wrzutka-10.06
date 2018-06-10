namespace Wrzutka.Migrations.Wrzutka
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        AuthorUserID = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FileID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Files");
        }
    }
}
