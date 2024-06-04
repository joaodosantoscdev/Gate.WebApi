namespace Gate.Application.DTOs.Request
{
    public class UpdateUnitRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}