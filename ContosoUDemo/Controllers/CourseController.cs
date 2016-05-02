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

namespace ContosoUDemo.Controllers
{
    /*mwilliams:  add role based authorization*/
    [Authorize(Roles = "admin")]

    public class CourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Course
        //public ActionResult Index()
        //{
        //    //The automatic scaffolding has specified eager loading for the Department navigation property by using the Include method.
        //    var courses = db.Courses.Include(c => c.Department);
        //    return View(courses.ToList());            
        //}

        //mwilliams:  Add department filter
        public ActionResult Index(int? SelectedDepartment)
        {
            //The automatic scaffolding has specified eager loading for the Department navigation property by using the Include method.
            //var courses = db.Courses.Include(c => c.Department);
            //return View(courses.ToList());

            //var departments = db.Departments.OrderBy(d => d.Name).ToList();
            //ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);
            //int departmentID = SelectedDepartment.GetValueOrDefault();

            //IQueryable<Course> courses = db.Courses
            //    .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
            //    .OrderBy(d => d.CourseID)
            //    .Include(d => d.Department);

            //mwilliams: converted to method(index and listing both use same code)
            IQueryable<Course> courses = GetCourses(SelectedDepartment);

            // var sql = courses.ToString(); //for debugging
            return View(courses.ToList());
        }

        [AllowAnonymous]

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            //mwilliams:  custom method to return sorted list of departments
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            //Call the PopulateDepartmentsDropDownList method without setting the selected item, 
            //because for a new course the department is not established yet:
            PopulateDepartmentsDropDownList();
            return View();
        }

        //mwilliams: custom method to return sorted list of departments
        private void PopulateDepartmentsDropDownList(object selectedDepartment=null)
        {
            var departmentsQuery = from d in db.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);

        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {

            //mwilliams:  added error handling (try..catch)

            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception /*ex*/)
            {
                //Log the error (uncomment ex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //mwilliams:  custom method to return sorted list of departments
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);

            //Set the selected item when they redisplay the page after an error:
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            //mwilliams:  custom method to return sorted list of departments
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }
        */
        /*mwillams:  protect from overposting 
         The scaffolder generated a Bind attribute and added the entity created by the model binder to the entity set with a Modified flag. 
         That code is no longer recommended because the Bind attribute clears out any pre-existing data in fields not listed in the Include parameter.

         As a best practice to prevent overposting, the fields that you want to be updateable by the Edit page are whitelisted in the 
         TryUpdateModel parameters. Currently there are no extra fields that you're protecting, but listing the fields that you want the 
         model binder to bind ensures that if you add fields to the data model in the future, they're automatically protected until you explicitly add them here. 
       */
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseToUpdate = db.Courses.Find(id);
            if (TryUpdateModel(courseToUpdate, "",
               new string[] { "Title", "Credits", "DepartmentID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception /* ex */)
                {
                    //Log the error (uncomment ex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(courseToUpdate);
        }



        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //mwilliams:Calling an Update Query
        //Perform bulk update such as changing the number of credits for every course
        //If the College has a large number of courses, it would be inefficient to retrieve them all as entities and change them individually.
        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                /* RAW SQL Query
                    Use the Database.SqlQuery method for queries that return types that aren't entities. 
                    The returned data isn't tracked by the database context, even if you use this method to retrieve entity types.
                */
                ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
            }
            return View();
        }

        //mwilliams:  add custom controller for non-authenticated (guest users)
        [AllowAnonymous]
        public ActionResult Listing(int? SelectedDepartment)
        {
            //The automatic scaffolding has specified eager loading for the Department navigation property by using the Include method.
            //var courses = db.Courses.Include(c => c.Department);
            //return View(courses.ToList());

            //mwilliams:  add selected department filter
            /*
            var departments = db.Departments.OrderBy(d => d.Name).ToList();
            ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);
            int departmentID = SelectedDepartment.GetValueOrDefault();

            IQueryable<Course> courses = db.Courses
                .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
                .OrderBy(d => d.CourseID)
                .Include(d => d.Department);
            */
            //mwilliams: converted to method(index and listing both use same code)
            IQueryable<Course> courses = GetCourses(SelectedDepartment);

            // var sql = courses.ToString(); //for debugging
            return View(courses.ToList());
        }



        //mwilliams: custom method to return sorted list of departments
        private IQueryable<Course> GetCourses(int? SelectedDepartment)
        {
            var departments = db.Departments.OrderBy(d => d.Name).ToList();
            ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);
            int departmentID = SelectedDepartment.GetValueOrDefault();

            IQueryable<Course> courses = db.Courses
                .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
                .OrderBy(d => d.CourseID)
                .Include(d => d.Department);

            return courses;

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
