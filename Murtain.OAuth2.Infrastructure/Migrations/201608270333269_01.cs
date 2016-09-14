namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MA.USER_ACCOUNT",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OPEN_ID = c.String(maxLength: 50),
                        NAME = c.String(maxLength: 50),
                        NICK_NAME = c.String(maxLength: 50),
                        BIRTHDAY = c.DateTime(),
                        TELPHONE = c.String(maxLength: 50),
                        EMAIL = c.String(maxLength: 50),
                        STREET = c.String(maxLength: 250),
                        CITY = c.String(maxLength: 50),
                        PROVINCE = c.String(maxLength: 50),
                        COUNTRY = c.String(maxLength: 50),
                        SEX = c.Byte(),
                        HEADIMAGE_URL = c.String(maxLength: 2000),
                        IDENTITY_NO = c.String(maxLength: 50),
                        PASSWORD = c.String(maxLength: 250),
                        SALT = c.String(maxLength: 50),
                        SUBJECT = c.String(nullable: false, maxLength: 50),
                        IS_DELETED = c.Boolean(nullable: false),
                        CREATED_TIME = c.DateTime(),
                        CREATED_USER = c.String(maxLength: 50),
                        CHANGED_USER = c.String(maxLength: 50),
                        CHANGED_TIME = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("MA.USER_ACCOUNT");
        }
    }
}
