using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class Category:CommonProp
    {

        public Guid CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
    }
}
