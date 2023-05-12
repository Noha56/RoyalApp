using System.ComponentModel;

namespace RoyalFinalApp.Models.SharedProp
{
    public class CommonProp
    {
        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }
        [DisplayName("Is Puplished")]
        public bool IsPuplished { get; set; }
    }
}
