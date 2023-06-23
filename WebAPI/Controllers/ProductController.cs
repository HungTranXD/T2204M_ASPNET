using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/product]")]
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
            var products = _context.Products.Select(p => DTOConverter.ProductToDTO(p)).ToList();
            return Ok(products);
        }
    }
}
