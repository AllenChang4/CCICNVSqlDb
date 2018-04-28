namespace CCICNVSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Families", "Congregation", c => c.String());
            AddColumn("dbo.Families", "Fellowship", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Families", "Fellowship");
            DropColumn("dbo.Families", "Congregation");
        }
    }
}
