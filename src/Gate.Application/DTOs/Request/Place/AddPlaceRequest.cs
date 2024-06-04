namespace Gate.Application.DTOs.Request
{
    public class AddPlaceRequest
    {
        public int ComplexId { get; set; }
        public int UnitId { get; set; }
        public int ResidentId { get; set; }
        public string Placename { get; set; }
        public string Type { get; set; }
    }
}