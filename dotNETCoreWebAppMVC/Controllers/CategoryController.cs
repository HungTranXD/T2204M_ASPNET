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

        public IActionResult Edit(int id)
        {
            var category = _dataContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new EditCategoryViewModel { Id = id, Name = category.Name});
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Categories.Update(new Category { Id = viewModel.Id, Name = viewModel.Name});
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
            
        }

        public IActionResult Delete(int id)
        {
            var category = _dataContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _dataContext.Categories.Remove(category);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("No image uploaded");
            }

            var path = "wwwroot/uploads";
            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(image.FileName);
            var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            image.CopyTo(new FileStream(upload, FileMode.Create));
            var rs = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(rs);


        }
    }
}
