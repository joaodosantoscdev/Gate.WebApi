namespace Gate.Application.DTOs.Response
{
    public class PlaceResponse
    {
        public int Id { get; set; }
        public string Placename { get; set; }
        public string Type { get; set; }
        public int UnitId { get; set; }
        public int ComplexId { get; set; }
    }
}