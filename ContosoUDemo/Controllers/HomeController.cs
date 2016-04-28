using ContosoUDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUDemo.Controllers
{
    public class HomeController : Controller
    {
        //mwilliams:  for departments
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            //return View();
            var departments = db.Departments
                .Where(d=>d.Name!="Temp")
                .OrderBy(d => d.Name).ToList();
            return View(departments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}