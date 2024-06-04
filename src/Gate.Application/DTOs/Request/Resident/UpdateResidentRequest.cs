namespace Gate.Application.DTOs.Request
{
    public class UpdateResidentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public int ComplexId { get; set; }
        public int UnitId { get; set; }
        public List<UpdateContactRequest> Contacts { get; set; }
    }
}