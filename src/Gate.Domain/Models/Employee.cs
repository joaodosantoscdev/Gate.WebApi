namespace Gate.Domain.Models
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public int Register { get; set; }
        public int UserId { get; set; }
        public int UnityId { get; set; }
        public string Cpf  { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}