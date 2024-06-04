
namespace Gate.Domain.Models
{
    public class ContactInfo : Entity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ResidentId { get; set; }
        public ResidentInfo Resident { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}