using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalFinalApp.Models
{
    public class Hall:CommonProp
    {
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        public string? HallName { get; set; }
 
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
        [Display(Name = "Price")]

        public decimal? Price { get; set; }
        [Display(Name = "Status")]

        public bool Status { get; set; }
        [Display(Name = "Image")]
        public string? Image { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? AvailableDate { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category Name")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }


        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Is Booked")]
        public bool IsBooked { get; set; }

    }
}
