using Booky.DataAccess.Repositoy;
using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Booky.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _unitOfWork.Product.Get(p => p.Id == id, includeProperties:"Category");
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
