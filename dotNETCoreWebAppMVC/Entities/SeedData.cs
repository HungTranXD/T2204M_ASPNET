using Microsoft.EntityFrameworkCore;

namespace dotNETCoreWebAppMVC.Entities;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DataContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<DataContext>>()))
        {
            // Look for any categories
            bool categoriesSeeded = context.Categories.Any();

            // Look for any products
            bool productsSeeded = context.Products.Any();

            if (categoriesSeeded && productsSeeded)
            {
                return; // Both categories and products have been seeded
            }

            // Add categories if not already seeded
            if (!categoriesSeeded)
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Smartphone" },
                    new Category { Name = "Laptop" },
                    new Category { Name = "Monitor" },
                    new Category { Name = "Mouse" },
                    new Category { Name = "Keyboard" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Add products if not already seeded
            if (!productsSeeded)
            {
                var categories = context.Categories.ToList(); // Retrieve the categories from the database
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Product 1",
                        Price = 1000,
                        Description = "Desciption for product 1",
                        CategoryId = categories[0].Id
                    },
                    new Product
                    {
                        Name = "Product 2",
                        Price = 500,
                        Description = "Desciption for product 2",
                        CategoryId = categories[1].Id
                    },
                    new Product
                    {
                        Name = "Product 3",
                        Price = 1200,
                        Description = "Desciption for product 3",
                        CategoryId = categories[3].Id
                    }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}

