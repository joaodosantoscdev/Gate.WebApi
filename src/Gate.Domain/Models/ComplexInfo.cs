
namespace Gate.Domain.Models
{
    public class ComplexInfo : BaseInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<UnitInfo> Unities { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}