using Gate.Domain.Enums;

namespace Gate.Application.DTOs.Request
{
    public class AddAccessRequest
    {
        public DateTime Date {get; set;}
        public AccessTypeEnum Type { get; set; }
        public int? ResidentId { get; set; }
        public int? PlaceId { get; set; }
        public int UnitId { get; set; }
    }
}