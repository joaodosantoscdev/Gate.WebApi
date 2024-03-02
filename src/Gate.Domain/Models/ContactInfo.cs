
namespace Gate.Domain.Models
{
    public class ContactInfo : BaseInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PersonId { get; set; }
        public PersonInfo Person { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}