using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly T2204mAspnetApiContext _context;

        public ProductController(T2204mAspnetApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Select(p => DTOConverter.ProductToDTO(p))
                .ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(DTOConverter.ProductToDTO(product));
        }

        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var product = new Product
            {
                Name = productDTO.Name,
                Thumbnail = productDTO.Thumbnail,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                Description = productDTO.Description,
                CreatedAt = productDTO.CreatedAt,
                CategoryId = productDTO.CategoryId,
                BrandId = productDTO.CategoryId
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            var createdProduct = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == product.Id);
            

            return CreatedAtAction(nameof(Detail), new {id = product.Id}, DTOConverter.ProductToDTO(createdProduct));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Name = productDTO.Name;
            product.Thumbnail = productDTO.Thumbnail;
            product.Price = productDTO.Price;
            product.Quantity = productDTO.Quantity;
            product.Description = product.Description;
            product.CategoryId = productDTO.CategoryId;
            product.BrandId = product.BrandId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
