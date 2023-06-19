using dotNETCoreWebAppMVC.Entities;
using dotNETCoreWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dotNETCoreWebAppMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            //var categories = _dataContext.Categories.ToList<Category>();
            var categories = _dataContext.Categories
                //.Where(c => c.Name.Contains("ash"))
                //.OrderByDescending(c => c.Name)
                .Include(c => c.Products)
                //.Take(1) //Lay 1 
                //.Skip(1) //Bo qua 1 (trang 2)
                .ToList<Category>();

            //ViewData["categories"] = categories;
            ViewData["test"] = "this is a test";
            //ViewBag.Categories = categories;

            return View(categories);
        }

        public IActionResult Create() 
        {
            return View();
            
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Categories.Add(new Category { Name = viewModel.Name });
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _dataContext.Categories.Find;
            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }

        

        public string Delete(int? id)
        {
            return "Id = " + id!;
        }
    }
}
