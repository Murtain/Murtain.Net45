namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MA.GLOBAL_SETTING",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 256),
                        DISPLAYNAME = c.String(nullable: false, maxLength: 256),
                        VALUE = c.String(),
                        DESCRIPTION = c.String(maxLength: 2000),
                        SCOPE = c.Int(nullable: false),
                        GROUP = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("MA.GLOBAL_SETTING");
        }
    }
}
