using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users=new List<string>();
        }
        public string? RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }
        public List<string>? Users { get; set; }
    }
}
