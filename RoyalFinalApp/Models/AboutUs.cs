using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class AboutUs
    {

        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
