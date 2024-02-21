namespace Gate.Domain.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; } = true;
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime CreationDate { get; set; }
    }
}