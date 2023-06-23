using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/brand")]
    public class BrandController : ControllerBase
    {
        private readonly T2204mAspnetApiContext _context;

        public BrandController(T2204mAspnetApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var brands = _context.Brands.Select(b => DTOConverter.BrandToDTO(b)).ToList();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(DTOConverter.BrandToDTO(brand));
        }

        [HttpPost]
        public IActionResult Create(BrandDTO data)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    Name = data.Name,
                    Logo = data.Logo
                };
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Details), new { id = brand.Id}, DTOConverter.BrandToDTO(brand));
            }
            return BadRequest();
        }
    }
}
