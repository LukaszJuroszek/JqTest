using JqTest.DAL;
using JqTest.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult GetEditedPerson(int id)
        {
            using (var context = new JqContext())
            {
                var model = context.People.FirstOrDefault(x => x.Id == id);
                if (model != null)
                    return PartialView("EditPerson", model);
                else
                    return Json(new { success = false, msg = $"Person with id: {id} dont exist" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetPersons()
        {
            using (var context = new JqContext())
            {
                var people = context.People.ToList().Select(x =>
                new
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    CreatedAt = x.CreatedAt.ToShortDateString()
                }).ToList();
                return Json(new { recordsTotal = people.Count(), data = people }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EditPerson(Person model)
        {
            using (var context = new JqContext())
            {
                var person = context.People.FirstOrDefault(x => x.Id == model.Id);
                if (person != null)
                {
                    try
                    {
                        person.FirstName = model.FirstName;
                        person.SecondName = model.SecondName;
                        context.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, msg = e.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return Json(new { success = false, msg = $"Person with id: {model.Id} dont exist" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
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
                        return Json(new { success = true, msg = $"Successfully deleted person with id:{id}" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, msg = $"Person with id: {id} dont exist" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.ToString() }, JsonRequestBehavior.AllowGet);
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
                    return Json(new { success = true, msg = $"Successfully added {model.FirstName}" }, JsonRequestBehavior.AllowGet);
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