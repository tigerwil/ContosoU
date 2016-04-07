//mwilliams
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//end mwilliams
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
using ContosoUDemo.ViewModels;

namespace TinyCollege.Controllers
{
    /*mwilliams:  add role based authorization*/
    [Authorize(Roles = "instructor")]
    public class InstructorCourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: InstructorCourse
        public ActionResult Index(int? courseID)
        {
            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentInstructor = manager.FindByEmail(User.Identity.GetUserName());//username and email are the same

                var viewModel = new InstructorIndexData();

                viewModel.Instructors = db.Instructors
                    .Include(i => i.Courses)
                .Where(i => i.Email == currentInstructor.Email);

                var instructor = viewModel.Instructors.Where(i => i.Email == currentInstructor.Email).Single();

                viewModel.Courses = viewModel.Instructors.Where(
                     i => i.ID == instructor.ID).Single().Courses;

                if (courseID != null)
                {
                    ViewBag.CourseID = courseID.Value;
                    var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                    db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                    foreach (Enrollment enrollment in selectedCourse.Enrollments)
                    {
                        db.Entry(enrollment).Reference(x => x.Student).Load();
                    }

                    viewModel.Enrollments = selectedCourse.Enrollments;

                    //mwilliams:  get course title for ui display
                    ViewBag.CourseTitle = selectedCourse.Title;
                    ViewBag.Instructor = instructor.FullName;
                }

                return View(viewModel);

                //original
                //var instructorCourses = db.Instructors.Include(i=>i.Courses).Where(i => i.Email == currentInstructor.Email);
                //return View(instructorCourses.ToList());
                //end original

            }
            else
            {
                //return HttpNotFound();
                return View("Error");
            }
        }

        // GET: InstructorCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            return View(enrollment);
        }

        // POST: InstructorCourse/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? EnrollmentID, int? courseID)
        {
            if (EnrollmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gradeToUpdate = db.Enrollments.Find(EnrollmentID);

            if (TryUpdateModel(gradeToUpdate, "", new string[] { "Grade" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index", "InstructorCourse", new { courseID = courseID });

                }
                catch (Exception /* dex */)
                {
                    //Log the error(uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(gradeToUpdate);
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
