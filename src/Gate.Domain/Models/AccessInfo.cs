using Gate.Domain.Enums;

namespace Gate.Domain.Models
{
    public class AccessInfo : Entity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AccessTypeEnum AccessType { get; set; }
        public int UnitId { get; set; }
        public int ResidentId { get; set; }
        public ResidentInfo Resident { get; set; }
        public int PlaceId { get; set; }
        public PlaceInfo Place  { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}