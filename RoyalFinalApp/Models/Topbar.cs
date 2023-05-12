using RoyalFinalApp.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace RoyalFinalApp.Models
{
    public class Topbar : CommonProp
    {
        public Guid Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Fb { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }

    }
}
