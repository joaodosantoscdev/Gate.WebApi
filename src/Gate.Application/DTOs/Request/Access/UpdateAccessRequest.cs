namespace Gate.Application.DTOs.Request
{
    public class UpdateAccessRequest
    {
        public int Id { get; set; }
        public DateTime Date {get; set;}
        public string Type { get; set; }
        public int ResidentId { get; set; }
        public int UnitId { get; set; }
    }
}