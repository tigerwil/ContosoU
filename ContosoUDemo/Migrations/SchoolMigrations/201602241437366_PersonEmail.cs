namespace ContosoUDemo.Migrations.SchoolMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "Email");
        }
    }
}
