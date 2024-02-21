namespace Gate.Domain.Models
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string? PrimaryColor { get; set; } = "#ffffff";
        public string? AccentColor { get; set; } = "#ffffff";
        public string? WarnColor { get; set; } ="#ffffff";
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}