namespace Basico.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class igpm_datas : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IGPMIndices", "dt_ini");
            DropColumn("dbo.IGPMIndices", "dt_fim");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IGPMIndices", "dt_fim", c => c.DateTime(nullable: false));
            AddColumn("dbo.IGPMIndices", "dt_ini", c => c.DateTime(nullable: false));
        }
    }
}
