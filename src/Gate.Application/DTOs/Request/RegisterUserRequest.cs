
namespace Gate.Application.DTOs.Request
{
    public class RegisterUserRequest
    {
        public string Description { get; set; }
        public string Username { get; set; }
        public string Cpf  { get; set; }
        public int Register  { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CompanyRequestInfo userCompany { get; set; } 
        public List<UnityRequestInfo> userUnityList { get; set; } 
    }
}