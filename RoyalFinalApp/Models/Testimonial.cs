using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class Testimonial : CommonProp
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }
        public bool Status { get; set; }
    }
}
