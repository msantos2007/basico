namespace Basico.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class igpm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IGPMIndices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ativo = c.Boolean(nullable: false),
                        dt_referencia = c.DateTime(nullable: false),
                        dt_ini = c.DateTime(nullable: false),
                        dt_fim = c.DateTime(nullable: false),
                        indice = c.Double(nullable: false),
                        dt_criacao = c.DateTime(),
                        usr_criacao = c.String(maxLength: 50),
                        dt_alteracao = c.DateTime(),
                        usr_alteracao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IGPMPrincipal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ativo = c.Boolean(nullable: false),
                        dt_ultima_execucao = c.DateTime(),
                        dt_criacao = c.DateTime(),
                        usr_criacao = c.String(maxLength: 50),
                        dt_alteracao = c.DateTime(),
                        usr_alteracao = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IGPMPrincipal");
            DropTable("dbo.IGPMIndices");
        }
    }
}
