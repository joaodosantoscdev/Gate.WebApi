using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Gate.Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Gate.Domain.Models;
using System.Runtime.InteropServices;
using Gate.Identity.Models;

namespace Gate.Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;            
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<RegisterUserResponse> RegisterIdentityUser(RegisterUserRequest registerUserRequest, int companyId) 
        {
            var identityUser = new ApplicationUser()
            {
                UserName = registerUserRequest.Email,
                Email = registerUserRequest.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);
            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            var registerUserResponse = new RegisterUserResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                registerUserResponse.AddErrors(result.Errors.Select(r => r.Description));
            else
                registerUserResponse.UserInfo = await GetByEmailAsync(identityUser.Email);
            
            return registerUserResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest) 
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginRequest.Email, userLoginRequest.Password, false, true);
            if (result.Succeeded)
                return await GenerateToken(userLoginRequest.Email);

            var userLoginResponse = new UserLoginResponse();
            if (!result.Succeeded) {
                if (result.IsLockedOut)
                    userLoginResponse.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    userLoginResponse.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    userLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    userLoginResponse.AddError("Usuário ou senha estão incorretos");
             }
                
            return userLoginResponse;
        }

        public async Task<bool> HasIdentityUser(string email) => await _userManager.FindByEmailAsync(email) != null;

        public async Task<UserInfo> GetByEmailAsync(string email) 
        { 
            var applicationUser = await _userManager.FindByEmailAsync(email);

            var userInfo = new UserInfo()
            {
                Username = applicationUser.UserName,
                Email = applicationUser.Email,
            };

            return userInfo;
        } 

        private async Task<UserLoginResponse> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await GetUserClaims(user);

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponse() {
                AccessToken = token,
                ExpirationDate = expirationDate
            };
        }

        private async Task<IList<Claim>> GetUserClaims(ApplicationUser user) 
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti,  new Guid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}