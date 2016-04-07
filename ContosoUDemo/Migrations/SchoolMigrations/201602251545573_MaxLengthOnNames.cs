namespace ContosoUDemo.Migrations.SchoolMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "LastName", c => c.String(maxLength: 65));
            AlterColumn("dbo.Person", "FirstMidName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "FirstMidName", c => c.String());
            AlterColumn("dbo.Person", "LastName", c => c.String());
        }
    }
}
