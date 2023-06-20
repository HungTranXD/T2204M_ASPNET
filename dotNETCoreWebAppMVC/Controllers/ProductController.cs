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

        public IActionResult Index(string searchString)
        {
            var products = from p in _dataContext.Products.Include(p => p.Category) select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            return View(products.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.categories = _dataContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string rs = null;
                if (viewModel.Image != null)
                {
                    var path = "wwwroot/uploads";
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(viewModel.Image.FileName);
                    var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                    viewModel.Image.CopyTo(new FileStream(upload, FileMode.Create));
                    rs = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                }

                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    Image = rs,
                    CategoryId = viewModel.CategoryId,
                };
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categories = _dataContext.Categories.ToList();
            return View(viewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var product = _dataContext.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
            };

            ViewBag.id = id;
            ViewBag.categories = _dataContext.Categories.ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {
                var product = _dataContext.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name= viewModel.Name;
                product.Price= viewModel.Price;
                product.Description= viewModel.Description;
                product.CategoryId= viewModel.CategoryId;

                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.id = id;
            ViewBag.categories = _dataContext.Categories.ToList();
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var product = _dataContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _dataContext.Products.Remove(product);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
