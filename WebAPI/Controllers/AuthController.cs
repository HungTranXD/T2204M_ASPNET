using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels;
using BCrypt.Net;
using dotNETCoreWebAppMVC.Entities;
using WebAPI.Entities;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly T2204mAspnetApiContext _context;

        public AuthController(T2204mAspnetApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegister registerData)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashed = BCrypt.Net.BCrypt.HashPassword(registerData.Password, salt);

            var user = new User
            {
                Email = registerData.Email,
                Fullname = registerData.Fullname,
                Password = hashed
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var UserDTO = new UserDTO
            {
                Email = user.Email,
                Fullname = user.Fullname,
                Token = null
            };

            return Ok(UserDTO);
        }
    }
}
