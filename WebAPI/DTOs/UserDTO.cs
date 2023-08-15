namespace WebAPI.DTOs
{
    public class UserDTO
    {
        /*public int Id { get; set; }*/

        public string Email { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string? Token { get; set; }
    }
}
