
namespace Gate.Application.DTOs.Request
{
    public class RegisterUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
    }
}