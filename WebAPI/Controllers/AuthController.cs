using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels;
using BCrypt.Net;
using dotNETCoreWebAppMVC.Entities;
using WebAPI.Entities;
using WebAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly T2204mAspnetApiContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(T2204mAspnetApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
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
                Token = generateJWT(user)
            };

            return Ok(UserDTO);
        }

        private string generateJWT (User user)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signatureKey = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,"user")
            };
            var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    payload,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
