namespace WebAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Thumbnail { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public CategoryDTO Category { get; set; }
        public BrandDTO Brand { get; set; }

    }
}
