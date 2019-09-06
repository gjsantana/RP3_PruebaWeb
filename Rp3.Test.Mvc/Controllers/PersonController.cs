using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rp3.Test.Mvc.Controllers
{
    public class PersonController : Controller
    {
        public static int? PersonId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["IdPerson"] != null)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["IdPerson"]);
                else
                    return null;
            }
            private set
            {
                if (value == null)
                    System.Web.HttpContext.Current.Session.Remove("IdPerson");
                else
                    System.Web.HttpContext.Current.Session["IdPerson"] = value;
            }
        }

        public static List<Models.PersonViewModel> Persons
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Persons"] != null)
                    return (List<Models.PersonViewModel>)System.Web.HttpContext.Current.Session["Persons"];
                else
                    return null;
            }
            private set
            {
                if (value == null)
                    System.Web.HttpContext.Current.Session.Remove("Persons");
                else
                    System.Web.HttpContext.Current.Session["Persons"] = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetAllPersons());
        }

        [ChildActionOnly]
        public ActionResult PersonSelection()
        {
            if (Persons == null)            
                Persons = GetAllPersons();

            return PartialView("PersonSelectionControl");
        }
        public ActionResult PersonSelected(string id)
        {
            var data = id.Split('-');
            if (int.TryParse(data[0], out int number))
                PersonId = number;

            return RedirectToAction(data[1], data[2]);
        }
        public ActionResult PersonUnSelected(string id)
        {
            PersonId = null;
            var data = id.Split('-');
            return RedirectToAction(data[0], data[1]);
        }

        private List<Models.PersonViewModel> GetAllPersons()
        {
            Rp3.Test.Proxies.Proxy proxy = new Proxies.Proxy();

            List<Rp3.Test.Mvc.Models.PersonViewModel> persons = proxy.GetPersons().
                Select(p => new Models.PersonViewModel()
                {
                    Active = p.Active,
                    PersonId = p.PersonId,
                    Identification = p.Identification,
                    Name = p.Name
                }).ToList();

            return persons.OrderBy(P => P.Name).ToList();
        }
    }
}