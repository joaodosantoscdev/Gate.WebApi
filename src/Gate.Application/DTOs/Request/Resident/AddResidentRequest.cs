namespace Gate.Application.DTOs.Request
{
    public class AddResidentRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Type { get; set; }
        public Guid Token { get; set; }
        public string DocumentNumber { get; set; }
        public int ComplexId { get; set; }
        public int UnitId { get; set; }
        public List<AddContactRequest> Contacts { get; set; }
    }
}