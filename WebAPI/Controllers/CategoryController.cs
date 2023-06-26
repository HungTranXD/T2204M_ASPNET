using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var categories = _context.Categories
                .Include(c => c.Products)
                    .ThenInclude(p => p.Brand)
                .Select(c => DTOConverter.CategoryToDTO(c))
                .ToList();
            return Ok(categories);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult Details(int id) 
        {
            var category = _context.Categories
                .Include(c => c.Products)
                    .ThenInclude(p => p.Brand)
                .FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(DTOConverter.CategoryToDTO(category));
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryDTO categoryDTO) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            category.Name = categoryDTO.Name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
