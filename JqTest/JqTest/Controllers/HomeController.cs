using JqTest.DAL;
using JqTest.Models;
using System;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult EditPerson(int id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult DeletePerson(int id)
        {
            try
            {
                using (var context = new JqContext())
                {
                    var person = context.People.SingleOrDefault(x => x.Id == id);
                    if (person != null)
                    {
                        context.Entry(person).State = EntityState.Deleted;
                        context.SaveChanges();
                        return Json(new { success = true, msg = "Successfully deleted person with id: " + id }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            msg = $"Person with id: {id} dont exist"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    msg = e.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddPerson(Person model)
        {
            try
            {
                using (var context = new JqContext())
                {
                    model.CreatedAt = DateTime.Now;
                    context.People.Add(model);
                    context.SaveChanges();
                    return Json(new { success = true, msg = "Successfully added " + model.FirstName }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.ToString() }, JsonRequestBehavior.AllowGet);
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