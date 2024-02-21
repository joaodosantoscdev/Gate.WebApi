using Microsoft.IdentityModel.Tokens;

namespace Gate.Identity.Configurations
{
    public class JwtOptions
    {
        public string  Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int Expiration { get; set; }
    }
}