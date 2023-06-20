using dotNETCoreWebAppMVC.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotNETCoreWebAppMVC.Models
{
    public class EditProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Toi thieu 6 ky tu")]
        [MaxLength(200, ErrorMessage = "Toi da 200 ky tu")]
        [Display(Name = "Ten san pham")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Gia tien")]
        public double Price { get; set; }

        [Display(Name = "Mo ta")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        /*[FileExtensions(Extensions = ".png,.jpg,.jpeg", ErrorMessage = "Please select a valid image file.")]*/
        public string? Image { get; set; }

        [Display(Name = "Danh muc")]
        public int CategoryId { get; set; }

    }
}
