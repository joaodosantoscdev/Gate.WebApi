namespace Gate.Domain.Models
{
    public class UnityInfo
    {
        public int Id { get; set; }
        public string TaxId { get; set; }
        public bool Active { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}