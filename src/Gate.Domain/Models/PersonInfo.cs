namespace Gate.Domain.Models
{
    public abstract class PersonInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public int ContactId { get; set; }
        public List<ContactInfo> Contacts { get; set; }
        public List<AccessInfo> Accesses { get; set; }
    }
}