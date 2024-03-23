namespace Gate.Domain.Models
{
    public class UserInfo : Entity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; } = true;
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}