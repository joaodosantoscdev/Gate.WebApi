using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Microsoft.Extensions.Options;
using Gate.Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Gate.Domain.Models;
using Gate.Identity.Models;
using Gate.Application.DTOs.Response.Interfaces;
using Gate.Identity.BusinessLogic.Interfaces;
using Gate.Identity.Constants;
using Gate.Identity.Context;
using Newtonsoft.Json;

namespace Gate.Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityDataContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(IdentityDataContext context,
                               SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole<int>> roleManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;            
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IBaseResponse<RegisterUserResponse>> RegisterIdentityUser(RegisterUserRequest registerUserRequest) 
        {
            if (await HasIdentityUser(registerUserRequest.Email))
                return BaseResponse.CreateErrorResponse<RegisterUserResponse>($"Usuário com E-mail {registerUserRequest.Email} já registrado.");

            var identityUser = new ApplicationUser()
            {
                UserName = registerUserRequest.Email,
                Description = registerUserRequest.Description,
                PhoneNumber = registerUserRequest.Phone,
                Email = registerUserRequest.Email,
                Birthdate = registerUserRequest.Birthdate,
                CreatedAt = DateTime.Now,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);
            if (result.Succeeded) { 
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
                await AddRoleToUserAsync(identityUser, Role.User);
            }

            var registerUserResponse = new RegisterUserResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                registerUserResponse.AddErrors(result.Errors.Select(r => r.Description));
            else
                registerUserResponse.UserInfo = await GetByEmailAsync(identityUser.Email);
            
            return BaseResponse.CreateSuccessResponse(registerUserResponse);
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

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);

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

        public async Task AddRoleToUserAsync(ApplicationUser user, string roleName)
        {
            // Verificar se o usuário existe
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Verificar se a role existe
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleName), $"Role '{roleName}' não encontrada.");
            }

            // Criar uma instância de ApplicationUserRole e definir os IDs
            var userRole = new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            // Adicionar a instância de ApplicationUserRole ao contexto e salvar as alterações
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        private async Task<IList<Claim>> GetUserClaims(ApplicationUser user) 
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
            claims.Add(new Claim("userInfo", JsonConvert.SerializeObject(user)));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}