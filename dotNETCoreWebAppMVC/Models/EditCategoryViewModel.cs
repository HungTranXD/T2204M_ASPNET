using System.ComponentModel.DataAnnotations;

namespace dotNETCoreWebAppMVC.Models
{
    public class EditCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ten danh muc")]
        public string Name { get; set; }
    }
}
