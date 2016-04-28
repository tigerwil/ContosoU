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
    public class PeopleController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
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
