using ContosoUDemo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUDemo.DAL
{
    public class SchoolContext: DbContext
    {
        //Constructor: Initialize ConnectionString (matches web.config)
        public SchoolContext():base("DefaultConnection")
        {
        }
        //Specifying Entity Sets - corresponding to database tables and each single
        //entity corresponds to a row in a table

        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        //mwilliams:  creating a more complex data model
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Department> Departments { get; set; }


        //Specifying singular table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //mwilliams:  using Fluent API
            /*
                For the many-to-many relationship between the Instructor and Course entities, the code specifies the 
                table and column names for the join table. Code First can configure the many-to-many relationship for you
                without this code, but if you don't call it, you will get default names such as InstructorInstructorID 
                for the InstructorID column.
            */
            modelBuilder.Entity<Course>()
               .HasMany(c => c.Instructors).WithMany(i => i.Courses)
               .Map(t => t.MapLeftKey("CourseID")
                   .MapRightKey("InstructorID")
                   .ToTable("CourseInstructor"));
        }
    }
}