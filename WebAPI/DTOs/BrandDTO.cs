namespace WebAPI.DTOs
{
    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
