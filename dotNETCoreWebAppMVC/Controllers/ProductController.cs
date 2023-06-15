using dotNETCoreWebAppMVC.Entities;
using dotNETCoreWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotNETCoreWebAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var products = _dataContext.Products
                .Include(p => p.Category)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            var selects = new SelectList(_dataContext.Categories.ToList());
            ViewBag.categories = selects;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    
                };
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
