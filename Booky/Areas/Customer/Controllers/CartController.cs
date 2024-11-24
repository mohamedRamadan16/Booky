using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Booky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booky.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(s => s.ApplicationUserId == UserId, includeProperties:"Product"),
                OrderTotal = 0
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += cart.Price * cart.Count;
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }




        public IActionResult Plus(int CartId)
        {
            ShoppingCart CartFromDb = _unitOfWork.ShoppingCart.Get(s => s.Id == CartId, tracked:true);
            CartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(CartFromDb);
            TempData["success"] = "Product Incremented Successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult Minus(int CartId)
        {
            ShoppingCart CartFromDb = _unitOfWork.ShoppingCart.Get(s => s.Id == CartId, tracked: true);
            if(CartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(CartFromDb);
            }
            else
            {
                CartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(CartFromDb);
                TempData["success"] = "Product Decremented Successfully";
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int CartId)
        {
            ShoppingCart CartFromDb = _unitOfWork.ShoppingCart.Get(s => s.Id == CartId, tracked: true);
            _unitOfWork.ShoppingCart.Remove(CartFromDb);
            TempData["success"] = "Product Removed Successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
