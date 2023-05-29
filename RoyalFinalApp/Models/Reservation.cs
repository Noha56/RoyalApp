using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Day { get; set; }
     
        public decimal? Total { get; set; }

        //public Receipt Receipt { get; set; }
        public Guid HallId { get; set; }

    }
}
