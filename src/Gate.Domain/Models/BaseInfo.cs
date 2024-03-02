namespace Gate.Domain.Models
{
    public interface BaseInfo
    {
        public int CreatedUser { get; set; } 
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}