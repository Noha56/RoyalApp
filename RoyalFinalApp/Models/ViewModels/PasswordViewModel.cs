using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models.ViewModels
{
    public class PasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string? CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]

        public string? NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The confirm password does not match, try again!")]
        public string? ConfirmPassword { get; set; }
    }
}
