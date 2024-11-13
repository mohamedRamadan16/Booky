﻿using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Booky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; // Class-level variable

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ProductVM productVM = new() { Product = new Product(), CategoryList = CategoryList };

            if(id == null || id == 0)
            {
                // create
                return View(productVM);
            }
            else
            {
                // update
                productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product, IFormFile? file)
        {

            if (!ModelState.IsValid || product == null)
            {
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                ProductVM productVM = new() { Product = new Product(), CategoryList = CategoryList };
                return View("Create", productVM);
            }

            if(product.CategoryId == 0)
            {
                ModelState.AddModelError("", "Select A Category");
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                ProductVM productVM = new() { Product = product, CategoryList = CategoryList };
                return View("Create", productVM);
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPaht = Path.Combine(wwwRootPath, @"images\Products");

                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    // delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                using (var fileStream = new FileStream(Path.Combine(productPaht, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                product.ImageUrl = @"\images\Products\" + fileName;
            }


            if (product.Id == 0)
            {
                // create
                _unitOfWork.Product.Add(product);
                TempData["success"] = "Product Created Successfully";
            }
            else
            {
                // update
                _unitOfWork.Product.Update(product);
                TempData["success"] = "Product Updated Successfully";
            }

            _unitOfWork.Save();
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

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                // delete the old image
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}