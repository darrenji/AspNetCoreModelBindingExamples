using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreModelBindingExamples.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreModelBindingExamples.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public IActionResult Index([FromQuery]int? id)
        {
            Person person;

            if(id.HasValue && (person=repository[id.Value]) != null)
            {
                return View(person);
            } else
            {
                return NotFound();
            }
        }

        public ViewResult Create() => View(new Person());

        [HttpPost]
        public ViewResult Create(Person model) => View("Index", model);

        public ViewResult DisplaySummary([Bind(nameof(AddressSummary.City),Prefix = nameof(Person.HomeAddress))]AddressSummary summary) => View(summary);

        public ViewResult Names(IList<string> names) => View(names ?? new List<string>());

        public ViewResult Address(IList<AddressSummary> addresses) => View(addresses ?? new List<AddressSummary>());

        //public string Header([FromHeader(Name ="Accept-Language")] string accept) => $"Header: {accept}";

        public ViewResult Header(HeaderModel model) => View(model);

    }
}
