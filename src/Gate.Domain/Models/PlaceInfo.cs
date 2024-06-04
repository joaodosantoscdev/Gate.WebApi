namespace Gate.Domain.Models
{
    public class PlaceInfo : Entity
    {
        public int Id { get; set; }
        public string Placename { get; set; }
        public string Type { get; set; }
        public int UnitId { get; set; }
        public UnitInfo Unit { get; set; }
        public int ComplexId { get; set; }
        public int? ResidentId { get; set; }
        public ResidentInfo Resident { get; set; }
        public List<AccessInfo> Accesses { get; set; }
    }
}