using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy;
using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Booky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Booky.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        // here in the ctor you ask the DI to inject a service that implements the ICategoryRepository
        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Company> companies = unitOfWork.Company.GetAll();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult ConfirmCreate(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", company);
            }
            unitOfWork.Company.Add(company);
            unitOfWork.Save();
            TempData["success"] = "Company Created Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();

            Company? company = unitOfWork.Company.Get(c => c.Id == Id);
            if (company == null)
            {
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult ConfirmEdit(Company company)
        {
            if (!ModelState.IsValid || company == null)
            {
                return View("Edit", company);
            }

            unitOfWork.Company.Update(company);
            unitOfWork.Save();
            TempData["success"] = "Company Updated Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            Company? company = unitOfWork.Company.Get(c => c.Id == Id);
            if (company == null)
            {
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Company company)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Company.Remove(company);
                unitOfWork.Save();
                TempData["success"] = "Company Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View("Delete", company);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Company> companies = unitOfWork.Company.GetAll();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.Company.Remove(companyToBeDeleted);
            unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });

        }
        #endregion
    }
}
