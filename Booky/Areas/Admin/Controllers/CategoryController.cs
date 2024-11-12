using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Booky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        // here in the ctor you ask the DI to inject a service that implements the ICategoryRepository
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = unitOfWork.Category.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult ConfirmCreate(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", category);
            }

            // custome validator
            if (category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not allowed in category name");
                return View("Create", category);
            }


            unitOfWork.Category.Add(category);
            unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();

            Category? category = unitOfWork.Category.Get(c => c.Id == Id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult ConfirmEdit(Category category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return View("Edit", category);
            }

            // custome validator
            if (category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not allowed in category name");
                return View("Edit", category);
            }

            unitOfWork.Category.Update(category);
            unitOfWork.Save();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            Category? category = unitOfWork.Category.Get(c => c.Id == Id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Remove(category);
                unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View("Delete", category);
        }
    }
}
