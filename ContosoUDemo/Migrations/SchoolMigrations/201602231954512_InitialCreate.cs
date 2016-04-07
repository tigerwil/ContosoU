namespace ContosoUDemo.Migrations.SchoolMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "Discriminator");
            RenameColumn(table: "dbo.Person", name: "Discriminator1", newName: "Discriminator");
            AlterColumn("dbo.Person", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Discriminator", c => c.String());
            RenameColumn(table: "dbo.Person", name: "Discriminator", newName: "Discriminator1");
            AddColumn("dbo.Person", "Discriminator", c => c.String());
        }
    }
}
