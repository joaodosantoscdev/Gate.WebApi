
namespace Gate.Application.DTOs.Response
{
    public class ResidentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhotoBase64 { get; set; }
        public string Type { get; set; }
        public Guid Token { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public List<ContactResponse>? Contacts { get; set; }
        public List<PlaceResponse>? Places { get; set; }
    }
}