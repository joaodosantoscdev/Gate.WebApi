namespace Gate.Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public int CreatedUser { get; set; } 
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}