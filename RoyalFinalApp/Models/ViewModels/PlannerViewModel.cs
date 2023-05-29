using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models.ViewModels
{
    public class PlannerViewModel:CommonProp
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }
        [Required]
        public string? PlannerName { get; set; }
        [Required]
        public string? Specialist { get; set; }
    }
}
