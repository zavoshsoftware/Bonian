﻿namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "AboutEn", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "AboutEn", c => c.String());
        }
    }
}
