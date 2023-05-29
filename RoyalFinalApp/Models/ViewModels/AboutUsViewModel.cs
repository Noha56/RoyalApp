namespace RoyalFinalApp.Models.ViewModels
{
    public class AboutUsViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
