namespace Gate.Domain.Models
{
    public class AccessInfo : BaseInfo
    {
        public int Id { get; set; }
        public DateTime Date {get; set;}
        public string Type { get; set; }
        public int PersonId { get; set; }
        public PersonInfo Person { get; set; }
        public int UnitId { get; set; }
        public UnitInfo Unit  { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}