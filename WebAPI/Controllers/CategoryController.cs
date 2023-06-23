using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly T2204mAspnetApiContext _context;

        public CategoryController(T2204mAspnetApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach(var item in categories)
            {
                list.Add(new CategoryDTO { Id = item.Id, Name = item.Name });
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult Details(int id) 
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                return Ok(new CategoryDTO { Id = category.Id, Name = category.Name });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO data) 
        {
            if(ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = data.Name
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Created($"detail?id={category.Id}", new CategoryDTO { Id = category.Id, Name = category.Name });
            } 
            return BadRequest();
        }
    }
}
