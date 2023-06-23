using WebAPI.Entities;

namespace WebAPI.DTOs
{
    public static class DTOConverter
    {
        public static ProductDTO ProductToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                // Map other properties
            };
        }

        public static CategoryDTO CategoryToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                // Map other properties
            };
        }

        public static BrandDTO BrandToDTO(Brand brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Logo = brand.Logo,
                // Map other properties
            };
        }
    }
}
