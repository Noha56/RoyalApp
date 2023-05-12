using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
