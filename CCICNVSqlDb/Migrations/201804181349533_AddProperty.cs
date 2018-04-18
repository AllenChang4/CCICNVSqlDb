namespace CCICNVSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Families", "ChildrenChineseName", c => c.String());
            AddColumn("dbo.Families", "eMail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Families", "eMail");
            DropColumn("dbo.Families", "ChildrenChineseName");
        }
    }
}
