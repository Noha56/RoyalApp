using Microsoft.Build.Framework;

namespace RoyalFinalApp.Models
{
    public class Navbar
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Background { get; set; }

    }
}
