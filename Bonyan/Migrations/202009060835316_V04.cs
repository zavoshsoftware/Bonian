namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V04 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        TitleEn = c.String(),
                        Code = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Body = c.String(storeType: "ntext"),
                        BodyEn = c.String(storeType: "ntext"),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blogs");
        }
    }
}
