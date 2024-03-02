

namespace Gate.Domain.Models
{
    public class GuestInfo : PersonInfo, BaseInfo
    {
        public int ResidentId { get; set; }
        public string Comment { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}