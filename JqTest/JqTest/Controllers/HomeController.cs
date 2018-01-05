using JqTest.Models;
using JqTest.Services;
using System.Linq;
using System.Web.Mvc;

namespace JqTest.Controllers
{
    public class HomeController : Controller
    {
        private PersonService _personService;

        public HomeController(PersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPerson(int id, string partialViewName)
        {
            var model = _personService.GetPersonById(id);
            if (model != null)
                return PartialView(partialViewName, model);
            else
                return Json(new { success = false, msg = $"Person with id: {id} dont exist" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPersons()
        {
            var people = _personService.GetAllPersons();
            return Json(new { recordsTotal = people.Count(), data = people }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditPerson(Person model)
        {
            var personUpdateResult = _personService.UpdatePerson(model);
            if (personUpdateResult)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, msg = "Error while updating Person" }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeletePerson(int id)
        {
            var personDeleteResult = _personService.DeletePerson(id);
            if (personDeleteResult)
                return Json(new { success = true, msg = $"Successfully deleted person with id:{id}" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, msg = "Error while deleting person" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddPerson(Person model)
        {
            var personAddResult = _personService.AddPerson(model);
            if (personAddResult)
                return Json(new { success = true, msg = $"Successfully added {model.FirstName}" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, msg = "Error while adding person" }, JsonRequestBehavior.AllowGet);
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