using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotNETCoreWebAppMVC.Entities

{
    [Table("Products")]
    public class Product
    {
        [Key] 
        public int Id { get; set; } //Abstract property

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        

    }
}
