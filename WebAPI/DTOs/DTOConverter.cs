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
                Thumbnail = product.Thumbnail,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                BrandId = product.BrandId,
                BrandName = product.Brand?.Name
            };
        }

        public static CategoryDTO CategoryToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(ProductToDTO).ToList()
            };
        }

        public static BrandDTO BrandToDTO(Brand brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Logo = brand.Logo,
                Products = brand.Products.Select(ProductToDTO).ToList()
            };
        }
    }
}
