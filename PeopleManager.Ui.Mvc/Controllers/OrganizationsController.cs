
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Organization organization)
        {
            _dbContext.Organizations.Add(organization);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var orginization = _dbContext.Organizations.ToList()
                .FirstOrDefault(p => p.Id == id);

            if (orginization == null)
            {
                return RedirectToAction("Index");
            }

            return View(orginization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] Organization orginization)
        {
            var dbOrginization = _dbContext.Organizations.ToList()
                .FirstOrDefault(p => p.Id == id);

            if (dbOrginization == null)
            {
                return RedirectToAction("Index");
            }

            dbOrginization.Name = orginization.Name;
            dbOrginization.Description = orginization.Description;
            dbOrginization.Id = id;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var orginization = _dbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            return View(orginization);
        }

        [HttpPost("/Organizations/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm([FromRoute] int id)
        {
            var organization = _dbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Organizations.Remove(organization);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");


        }
    }
}
