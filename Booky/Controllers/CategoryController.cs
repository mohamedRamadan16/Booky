using Booky.Data;
using Booky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Booky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public CategoryController(ApplicationDbContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public IActionResult Index()
        {
            List<Category> categories = DbContext.Categories.ToList();
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


            DbContext.Categories.Add(category);
            DbContext.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");   
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();

            Category? category = DbContext.Categories.FirstOrDefault(c => c.Id == Id);
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

            DbContext.Categories.Update(category);
            DbContext.SaveChanges();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            Category? category = DbContext.Categories.FirstOrDefault(c => c.Id == Id);
            if(category == null)
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
                DbContext.Categories.Remove(category);
                DbContext.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View("Delete" ,category);
        }
    }
}
