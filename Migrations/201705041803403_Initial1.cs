namespace lab4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "EngineType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "EngineType");
        }
    }
}
