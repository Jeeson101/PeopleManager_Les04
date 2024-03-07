using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly PeopleManagerDbContext _dbContext;

        public OrganizationsController(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var organizations = _dbContext.Organizations.ToList();
            return View(organizations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Organization organization)
        {
            _dbContext.Organizations.Add(organization);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
