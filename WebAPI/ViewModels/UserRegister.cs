using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class UserRegister
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Fullname { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }
}
