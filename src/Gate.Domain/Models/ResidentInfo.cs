
namespace Gate.Domain.Models
{
    public class ResidentInfo : PersonInfo, BaseInfo
    {
        public int ComplexId { get; set; }
        public int UnitId { get; set; }
        public List<UnitResidentInfo> UnitResidents { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}