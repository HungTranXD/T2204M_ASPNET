using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace dotNETCoreWebAppMVC.Models
{
    public class CategoryViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(6, ErrorMessage = "Vui long nhap toi thieu 6 ky tu")]
        [MaxLength(255)]
        [Display(Name="Ten danh muc")]
        public string Name { get; set; }
    }
}
