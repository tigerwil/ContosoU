using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUDemo.DAL;
using ContosoUDemo.Models;
using PagedList;
using ContosoUDemo.ViewModels;

namespace ContosoUDemo.Controllers
{
    /*mwilliams:  add role based authorization*/
    [Authorize(Roles = "admin")]

    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        //public ActionResult Index()
        //{
        //    return View(db.Students.ToList());
        //}
        /*mwilliams:  Added Sorting, filtering and paging */
        public ActionResult Index(string sortOrder, string searchString, int? page, string currentFilter)//Sorting, filtering and paging
        {

            //mwilliams: for paging
            ViewBag.CurrentSort = sortOrder;
            //mwilliams: for sorting
            ViewBag.LNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FNameSortParm = sortOrder == "fname" ? "fname_desc" : "fname";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //mwilliams: for filtering
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            //get students
            var students = from s in db.Students
                           select s;

            //mwilliams: for filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }                

            //mwilliams:  apply sort
            switch (sortOrder)
            {
                case "lname_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "fname":
                    students = students.OrderBy(s => s.FirstMidName);
                    break;
                case "fname_desc":
                    students = students.OrderByDescending(s => s.FirstMidName);
                    break;
                case "email":
                    students = students.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    students = students.OrderByDescending(s => s.Email);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            //mwilliams: for paging
            //return View(students.ToList());
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        /* mwilliams
          The ValidateAntiForgeryToken attribute helps prevent cross-site request forgery attacks. 
          It requires a corresponding Html.AntiForgeryToken() statement in the view
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,Email,EnrollmentDate")] Student student)
        {
            /*mwilliams:  exception handling (try...catch)
              and remove ID from the Bind attribute for the scaffolded method  because ID is the primary key value which 
              SQL Server will set automatically when the row is inserted. Input from the user does not set the ID value            
            */

            try
            {
                if (ModelState.IsValid)
                {
                    db.People.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /*dex*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            /*  mwilliams
                DataException exceptions are sometimes caused by something external to the application rather than a programming error, 
                so the user is advised to try again. 
                
                Although not implemented in this sample, a production quality application would log the exception. 
            */


            return View(student);
        }

        // GET: Student/Edit/5
        /* mwilliams */
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        public ActionResult Edit(int? id, int? page)//mwilliams adding paging sticky
        {
            ViewBag.CurrentPage = page;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students
                .Include(i => i.Enrollments)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedCourseData(student);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        private void PopulateAssignedCourseData(Student student)
        {
            var allCourses = db.Courses;
            var studentCourses = new HashSet<int>(student.Enrollments.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = studentCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }
        /* end mwilliams */

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,Email,EnrollmentDate")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(student);
        //}

        /*mwillams:  protect from overposting 
          The scaffolder generated a Bind attribute and added the entity created by the model binder to the entity set with a Modified flag. 
          That code is no longer recommended because the Bind attribute clears out any pre-existing data in fields not listed in the Include parameter.
          
            As a best practice to prevent overposting, the fields that you want to be updateable by the Edit page are whitelisted in the 
            TryUpdateModel parameters. Currently there are no extra fields that you're protecting, but listing the fields that you want the 
            model binder to bind ensures that if you add fields to the data model in the future, they're automatically protected until you explicitly add them here. 
        */
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var studentToUpdate = db.Students.Find(id);
            //var studentToUpdate = db.Students
            //   .Include(s => s.Enrollments)
            //   .Where(i => i.ID == id)
            //   .Single();

            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "LastName", "FirstMidName", "Email","EnrollmentDate" }))
            {
                try
                {
                    //mwilliams:  enrollment
                    UpdateStudentCourses(selectedCourses, studentToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException dex )
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    ModelState.AddModelError("", dex.InnerException.Message);
                }
            }
            PopulateAssignedCourseData(studentToUpdate);
            return View(studentToUpdate);
  
        }

        private void UpdateStudentCourses(string[] selectedCourses, Student studentToUpdate)
        {
            if (selectedCourses == null)//all courses are checked off - remove from data storage
            {
                // studentToUpdate.Enrollments = new List<Enrollment>();
                // return;
                var enrollment = db.Enrollments.Where(e => e.StudentID == studentToUpdate.ID).ToList();
                db.Enrollments.RemoveRange(enrollment);
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            //var studentCourses = new HashSet<int>
            //    (studentToUpdate.Enrollments.Select(e => e.CourseID));
            var studentCourses = new HashSet<int>
               (studentToUpdate.Enrollments.Where(e => e.Student.ID == studentToUpdate.ID).Select(e => e.CourseID));

            //mwilliams: added ToList() to prevent error: There is already an open DataReader associated with this Command which must be closed first. 
            //ToList() forces the DataContext to complete the read operation, freeing the connection up to perform another action.
            // other solution is to adde the MultipleActiveResultSets=True; within the connectionstring in web.config
            foreach (var course in db.Courses.ToList())
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!studentCourses.Contains(course.CourseID))
                    {
                        // studentToUpdate.Enrollments.Add(course);
                        Enrollment enrollment = new Enrollment();
                        enrollment.CourseID = course.CourseID;
                        enrollment.StudentID = studentToUpdate.ID;
                        studentToUpdate.Enrollments.Add(enrollment);
                    }
                }
                else
                {
                    if (studentCourses.Contains(course.CourseID))
                    {
                        //var enrollment = db.Enrollments.Where(e => e.StudentID == studentToUpdate.ID && e.Course.CourseID==course.CourseID).ToList();
                        //db.Enrollments.RemoveRange(enrollment);
                        Enrollment enrollment = db.Enrollments.Where(e => e.CourseID == course.CourseID && e.StudentID == studentToUpdate.ID).Single();
                        db.Enrollments.Remove(enrollment);
  

                    }
                }
            }
        }


        // GET: Student/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Student student = db.Students.Find(id);
        //    db.People.Remove(student);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


        //mwilliams:  Student Stats - using ViewModel(EnrollmentDateGroup)
        //public ActionResult Stats()
        //{
        //    IQueryable<ViewModels.EnrollmentDateGroup> data = from student in db.Students
        //                                                      group student by student.EnrollmentDate into dateGroup
        //                                                      select new ViewModels.EnrollmentDateGroup()
        //                                                      {
        //                                                          EnrollmentDate = dateGroup.Key,
        //                                                          StudentCount = dateGroup.Count()
        //                                                      };
        //    return View(data.ToList());
        //}
        public ActionResult Stats(string sortOrder)
        {
            //added sorting
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.StudentSortParm = sortOrder == "student" ? "student_desc" : "student";

            //get enrollment stats grouping results
            IQueryable<ViewModels.EnrollmentDateGroup> data = from student in db.Students
                                                              group student by student.EnrollmentDate into dateGroup
                                                              select new ViewModels.EnrollmentDateGroup()
                                                              {
                                                                  EnrollmentDate = dateGroup.Key,
                                                                  StudentCount = dateGroup.Count()
                                                              };

            // SQL version of the above LINQ code.
            //string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
            //    + "FROM Person "
            //    + "WHERE Discriminator = 'Student' "
            //    + "GROUP BY EnrollmentDate";
            //IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);

            //do sort
            switch (sortOrder)
            {
                case "date_desc":
                    data = data.OrderByDescending(d => d.EnrollmentDate);
                    break;
                case "student":
                    data = data.OrderBy(d => d.StudentCount);
                    break;
                case "student_desc":
                    data = data.OrderByDescending(d => d.StudentCount);
                    break;
                default:  // Name ascending 
                    data = data.OrderBy(d => d.EnrollmentDate);
                    break;
            }
            //return data to list
            return View(data.ToList());
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
