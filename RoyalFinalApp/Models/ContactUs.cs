using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class ContactUs
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }
        [Required]
        public string? Subject { get; set; }
    }
}
