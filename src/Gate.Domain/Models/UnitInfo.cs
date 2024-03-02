namespace Gate.Domain.Models
{
    public class UnitInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<UnitResidentInfo> UnitResidents { get; set; }
        public int ComplexId { get; set; }
        public ComplexInfo Complex { get; set; }
        public List<AccessInfo> Accesses { get; set; }
    }
}