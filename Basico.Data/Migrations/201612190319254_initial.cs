namespace Basico.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.identityRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        dt_criacao = c.DateTime(),
                        usr_criacao = c.String(maxLength: 50),
                        dt_alteracao = c.DateTime(),
                        usr_alteracao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.identityUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Firstname = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        IsLocked = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        dt_criacao = c.DateTime(),
                        usr_criacao = c.String(maxLength: 50),
                        dt_alteracao = c.DateTime(),
                        usr_alteracao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.identityUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        identityUserID = c.Int(nullable: false),
                        identityRoleID = c.Int(nullable: false),
                        dt_criacao = c.DateTime(),
                        usr_criacao = c.String(maxLength: 50),
                        dt_alteracao = c.DateTime(),
                        usr_alteracao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.identityRole", t => t.identityRoleID, cascadeDelete: true)
                .ForeignKey("dbo.identityUser", t => t.identityUserID, cascadeDelete: true)
                .Index(t => t.identityUserID)
                .Index(t => t.identityRoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.identityUserRole", "identityUserID", "dbo.identityUser");
            DropForeignKey("dbo.identityUserRole", "identityRoleID", "dbo.identityRole");
            DropIndex("dbo.identityUserRole", new[] { "identityRoleID" });
            DropIndex("dbo.identityUserRole", new[] { "identityUserID" });
            DropTable("dbo.identityUserRole");
            DropTable("dbo.identityUser");
            DropTable("dbo.identityRole");
            DropTable("dbo.Error");
        }
    }
}
