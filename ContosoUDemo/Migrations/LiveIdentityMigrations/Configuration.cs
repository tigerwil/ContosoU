namespace ContosoUDemo.Migrations.LiveIdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUDemo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\LiveIdentityMigrations";
        }

        protected override void Seed(ContosoUDemo.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //mwilliams 
            //add admin role
            if (!(context.Roles.Any(r => r.Name == "admin")))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roletoInsert = new IdentityRole { Name = "admin" };
                roleManager.Create(roletoInsert);
            }
            //mwilliams 
            //add student role
            if (!(context.Roles.Any(r => r.Name == "student")))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roletoInsert = new IdentityRole { Name = "student" };
                roleManager.Create(roletoInsert);
            }

            //mwilliams 
            //add instructor role
            if (!(context.Roles.Any(r => r.Name == "instructor")))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roletoInsert = new IdentityRole { Name = "instructor" };
                roleManager.Create(roletoInsert);
            }

            //add admin user and assign admin role
            if (!(context.Users.Any(u => u.UserName == "admin@contoso.com")))
            {
                var userStore = new UserStore<Models.ApplicationUser>(context);
                var userManager = new UserManager<Models.ApplicationUser>(userStore);
                var userToInsert = new Models.ApplicationUser { UserName = "admin@contoso.com", Email = "admin@contoso.com", EmailConfirmed = true };
                userManager.Create(userToInsert, "Admin@123456");
                userManager.AddToRole(userToInsert.Id, "admin");
            }

            //end mwilliams
        }
    }
}
