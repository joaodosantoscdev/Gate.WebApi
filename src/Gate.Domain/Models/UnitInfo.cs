namespace Gate.Domain.Models
{
    public class UnitInfo : Entity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int ComplexId { get; set; }
        public ComplexInfo Complex { get; set; }
        public List<PlaceInfo> Places { get; set; }
    }
}