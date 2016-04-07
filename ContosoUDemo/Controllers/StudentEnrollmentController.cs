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
//mwilliams
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//end mwilliams

namespace ContosoUDemo.Controllers
{
    /*mwilliams:  add role based authorization*/
    [Authorize(Roles = "student")]
    public class StudentEnrollmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: StudentEnrollment
        public ActionResult Index()
        {
            //get logged in user's id
            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                //locate this user within student entity
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentStudent = manager.FindByEmail(User.Identity.GetUserName());//username and email are the same

                Student student = db.Students
                    .Include(i => i.Enrollments)
                    .Where(i => i.Email == currentStudent.Email).Single();

                // Create and execute raw SQL query:  Get all courses that this student i. 
                string query = "SELECT CourseID, Title FROM Course WHERE CourseID NOT IN(SELECT DISTINCT CourseID FROM Enrollment WHERE StudentID = @p0)";
                IEnumerable<ViewModels.AssignedCourseData> data = db.Database.SqlQuery<ViewModels.AssignedCourseData>(query, student.ID);
                ViewBag.Courses = data.ToList();


                var studentEnrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).Where(e => e.Student.Email == currentStudent.Email);
                return View(studentEnrollments.ToList());
            }
            else
            {
                //return HttpNotFound();
                return View("Error");

            }
        }
        

        // GET: StudentEnrollment/Enroll
        public ActionResult Enroll(int? courseId)
        {

            if (courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentStudent = manager.FindByEmail(User.Identity.GetUserName());//username and email are the same

                Student student = db.Students
                    .Include(i => i.Enrollments)
                    .Where(i => i.Email == currentStudent.Email).Single();
                ViewBag.StudentID = student.ID;

                var studentEnrollments = new HashSet<int>(db.Enrollments.Include(e => e.Course).Include(e => e.Student).Where(e => e.Student.Email == currentStudent.Email).Select(e => e.CourseID));
                //fix for dealing with conversion int? to int
                int currentCourseID;
                if (courseId.HasValue)
                {
                    currentCourseID = (int)courseId;
                }
                else
                {
                    currentCourseID = 0;
                }
                //end of conversion fix

                if (studentEnrollments.Contains(currentCourseID))
                {
                    //return HttpNotFound();
                    //ViewBag.AlreadyEnrolledError = "You are already enrolled in this course";
                    ModelState.AddModelError("AlreadyEnrolled", "You are already enrolled in this course");
                }

            }

            Course course = db.Courses.Find(courseId);
            if (course == null)
            {
                return HttpNotFound();
            }


            return View(course);
        }


        // POST: StudentEnrollment/Enroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll([Bind(Include = "CourseID,StudentID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
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
