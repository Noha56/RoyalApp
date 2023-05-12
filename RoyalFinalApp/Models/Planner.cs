using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class Planner : CommonProp
    {
        public Guid Id { get; set; }
        [Required]
        public string? PlannerImg { get; set; }
        [Required]
        public string? PlannerName { get; set; }
        [Required]
        public string? Specialist { get;set; }
    }
}
