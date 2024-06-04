using Gate.Domain.Enums;

namespace Gate.Application.DTOs.Request.Access
{
    public class FastAccessRequest
    {
      public string Name { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDate { get; set; }
      public string DocumentNumber { get; set; }
      public AccessTypeEnum AccessType { get; set; }
      public string Type { get; set; }
      public int PlaceId { get; set; }
      public int UnitId { get; set; }
      public string PhotoBase64 { get; set; }
      public List<AddContactRequest> Contacts { get; set; }
    }
}