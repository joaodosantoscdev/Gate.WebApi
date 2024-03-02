
namespace Gate.Domain.Models
{
  public class UnitResidentInfo : BaseInfo
  {
    public int UnitId { get; set; }
    public UnitInfo Unit { get; set; }
    public int ResidentId { get; set; }
    public ResidentInfo Resident { get; set; }
    public int CreatedUser { get; set; }
    public int UpdatedUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    }
}