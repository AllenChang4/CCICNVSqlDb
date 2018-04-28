namespace CCICNVSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FamilyName = c.String(),
                        Parent = c.String(),
                        ChineseName = c.String(),
                        Children = c.String(),
                        ChildrenChineseName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Phone = c.String(),
                        eMail = c.String(),
                        FamilyPicture = c.Binary(),
                        Done = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Families");
        }
    }
}
