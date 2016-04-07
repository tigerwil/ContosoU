namespace ContosoUDemo.Migrations.SchoolMigrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUDemo.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\SchoolMigrations";
        }

        protected override void Seed(ContosoUDemo.DAL.SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            //================================================  INITIAL SEED =========================================================
            // SEED 1
            //  Initial Seed Method- can only be executed once (initial update-database command)
            //  Need to comment out after or using alternate seed with AddOrUpdate 
            // =======================================================================================================================

            /*
            //1.Add students
           var students = new List<Student>
          {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="calexander@contoso.com"},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="malonso@contoso.com"},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="aanand@contoso.com"},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="gbarzdukas@contoso.com"},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="yli@contoso.com"},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), Email="pjustice@contoso.com"},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="lnorman@contoso.com"},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="nolivetto@contoso.com"}
          };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            //2. Add Instructors
            var instructors = new List<Instructor>
            {
                new Instructor {FirstMidName="Marc",LastName="Williams",HireDate=DateTime.Parse("1996-01-31"), Email="mwilliams@contoso.com" },
                new Instructor {FirstMidName="Frank",LastName="Bekkering",HireDate=DateTime.Parse("1997-08-30") , Email="fbekkering@contoso.com"}
            };
            instructors.ForEach(i => context.Instructors.Add(i));
            context.SaveChanges();

            //3.  Add Courses
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            //4. Add Enrollments
            var enrollments = new List<Enrollment>
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();

            // ==========================================END  INITIAL SEED ============================================================
            */



            //========================================== ALTERNATE SEED ==============================================================
            // SEED 2:
            // Alterate seed method that will work every time update-database is issued
            // Initial Seed Method- can only be executed once (initial update-database command)
            // =======================================================================================================================
         /*

            //1.Add students
            var students = new List<Student>
          {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="calexander@contoso.com"},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="malonso@contoso.com"},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="aanand@contoso.com"},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="gbarzdukas@contoso.com"},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="yli@contoso.com"},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), Email="pjustice@contoso.com"},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="lnorman@contoso.com"},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="nolivetto@contoso.com"}
          };
            //mwilliams:  The first parameter passed to the AddOrUpdate method specifies the property to use to check if a row already exists. 
            //For the test student data that you are providing, the email property can be used for this purpose since each email in the list is unique
            students.ForEach(s => context.Students.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //2. Add Instructors
            var instructors = new List<Instructor>
            {
                new Instructor {FirstMidName="Marc",LastName="Williams",HireDate=DateTime.Parse("1996-01-31"), Email="mwilliams@contoso.com" },
                new Instructor {FirstMidName="Frank",LastName="Bekkering",HireDate=DateTime.Parse("1997-08-30") , Email="fbekkering@contoso.com"}
            };

            //mwilliams:  The first parameter passed to the AddOrUpdate method specifies the property to use to check if a row already exists. 
            //For the test instructor data that you are providing, the email property can be used for this purpose since each email in the list is unique
            instructors.ForEach(i => context.Instructors.AddOrUpdate(p => p.Email, i));
            context.SaveChanges();

            //3.  Add Courses
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            //courses.ForEach(s => context.Courses.Add(s));
            courses.ForEach(c => context.Courses.AddOrUpdate(ci => ci.CourseID, c));
            context.SaveChanges();

            //4. Add Enrollments
            var enrollments = new List<Enrollment>
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            //enrollments.ForEach(s => context.Enrollments.Add(s));
            //check if enrollment exists for each student via student id property
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    //no existing enrollment - add a new one
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();
            */
            // ==========================================END  INITIAL SEED ============================================================


            //================================================  ALTERNATE SEED =========================================================
            // SEED 3:  After complex data model changes
            // =========================================================================================================================
            //1.Add students
            var students = new List<Student>
          {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="calexander@contoso.com"},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="malonso@contoso.com"},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="aanand@contoso.com"},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="gbarzdukas@contoso.com"},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), Email="yli@contoso.com"},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), Email="pjustice@contoso.com"},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), Email="lnorman@contoso.com"},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), Email="nolivetto@contoso.com"}
          };
            //mwilliams:  The first parameter passed to the AddOrUpdate method specifies the property to use to check if a row already exists. 
            //For the test student data that you are providing, the email property can be used for this purpose since each email in the list is unique
            students.ForEach(s => context.Students.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //2. Add Instructors (modified to add more instructors)
            var instructors = new List<Instructor>
            {
                new Instructor {FirstMidName="Marc",LastName="Williams",HireDate=DateTime.Parse("1996-01-31"), Email="mwilliams@faculty.contoso.com" },
                new Instructor {FirstMidName="Frank",LastName="Bekkering",HireDate=DateTime.Parse("1997-08-30") , Email="fbekkering@contoso.com"},
                //more instructors added
                new Instructor { FirstMidName = "Kim",LastName = "Abercrombie",HireDate = DateTime.Parse("1995-03-11"), Email="kabercrombie@faculty.contoso.com"},
                new Instructor { FirstMidName = "Fadi",LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06"), Email="ffakhouri@faculty.contoso.com"},
                new Instructor { FirstMidName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01"), Email="rharui@faculty.contoso.com"},
                new Instructor { FirstMidName = "Candace",LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15"), Email="ckapoor@faculty.contoso.com"},
                new Instructor { FirstMidName = "Roger",LastName = "Zheng",HireDate = DateTime.Parse("2004-02-12"), Email="rzheng@faculty.contoso.com"}
            };

            //mwilliams:  The first parameter passed to the AddOrUpdate method specifies the property to use to check if a row already exists. 
            //For the test instructor data that you are providing, the email property can be used for this purpose since each email in the list is unique
            instructors.ForEach(i => context.Instructors.AddOrUpdate(p => p.Email, i));
            context.SaveChanges();

            //3. Add Departments (this is a new step)
            var departments = new List<Department>
            {
                new Department { Name = "English", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"),InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,StartDate = DateTime.Parse("2007-09-01"),InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //4.  Add Courses (modified to include departmentid and the ability to add Instructor entities that are related to this Course 
            //    by using the Instructors.Add method. If you didn't create an empty list, you wouldn't be able to add these relationships, 
            //    because the Instructors property would be null and wouldn't have an Add method.
            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Instructors = new List<Instructor>()
                },
            }; 
            //courses.ForEach(s => context.Courses.Add(s));
            courses.ForEach(c => context.Courses.AddOrUpdate(ci => ci.CourseID, c));
            context.SaveChanges();

            //4.  Add Office Assignments (New step)
            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,Location = "Smith 17" },
                new OfficeAssignment {InstructorID = instructors.Single( i => i.LastName == "Harui").ID,Location = "Gowan 27" },
                new OfficeAssignment {InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            //5.  Assign Instructors to Courses (New Step)
            //First create the AddOrUpdateInstructor Method below

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            //6. Add Enrollments (modified to match studentid by lastname and courseid by title)
            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };
            //enrollments.ForEach(s => context.Enrollments.Add(s));
            //check if enrollment exists for each student via student id property
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    //no existing enrollment - add a new one
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();


        }//end of Seed method

        //For step 5:   Assign Instructors to Courses
        void AddOrUpdateInstructor(ContosoUDemo.DAL.SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    }
}
