
namespace Gate.Domain.Models
{
    public class ResidentInfo : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? PhotoBase64 { get; set; }
        public string Type { get; set; }
        public Guid Token { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public List<ContactInfo> Contacts { get; set; }
        public List<AccessInfo> Accesses { get; set; }
        public List<PlaceInfo> Places { get; set; }
        public int UnitId { get; set; }
        public int ComplexId { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}