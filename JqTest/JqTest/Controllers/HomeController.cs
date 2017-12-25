using JqTest.DAL;
using JqTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPersons()
        {
            using (var context = new JqContext())
            {
                var people = context.People.ToList();
                var model = new DataTable
                {
                    data = people,
                    recordsTotal = people.Count()
                };
                return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
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