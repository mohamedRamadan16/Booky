using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult ConfirmCreate(Product product)
        {
            if (!ModelState.IsValid || product == null)
            {
                return View("Create", product);
            }
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id is null || Id == 0)
                return NotFound();

            Product product = _unitOfWork.Product.Get(p => p.Id == Id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? Id)
        {
            if(Id == 0 || Id is null)
                return NotFound();

            Product product = _unitOfWork.Product.Get(p => p.Id == Id);
            if (!ModelState.IsValid || product == null)
                return View("Delete", product);

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? Id)
        {
            if (Id is null || Id == 0)
                return NotFound();

            Product product = _unitOfWork.Product.Get(p => p.Id == Id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult ConfirmEdit(Product product)
        {
            if (!ModelState.IsValid || product == null)
                return View("Edit", product);

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Updated Successfully";
            return RedirectToAction("Index");
        }
    }
}
