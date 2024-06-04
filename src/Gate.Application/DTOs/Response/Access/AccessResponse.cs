using Gate.Domain.Enums;

namespace Gate.Application.DTOs.Response
{
    public class AccessResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AccessTypeEnum AccessType { get; set; }
        public string ComplexDescription { get; set; }
        public string UnitDescription { get; set; }
        public ResidentResponse Resident { get; set; }
        public PlaceResponse Place { get; set; }
    }
}