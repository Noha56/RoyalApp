namespace RoyalFinalApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        //Id = Reservation Id
        public int ReservationId { get; set; }
        public int HallId { get; set; }
        public double TotalPrice { get; set; }
        //public int PaymentId { get; set; }
       // public virtual Payment Payment { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Hall Hall { get; set; }
    }
}
