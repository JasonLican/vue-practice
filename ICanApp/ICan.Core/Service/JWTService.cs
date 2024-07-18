using AutoMapper;
using ICan.Core.EFDbContext;
using ICan.Core.IService;
using ICan.Module;
using ICan.Module.Entity;
using ICan.Module.ReuqestModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.Service
{
    public class JWTService : IJWTService
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public JWTService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _userService = userService;
        }
        public async Task<BaseResponse> CreateCredential(SysUser user)
        {
            var response = new BaseResponse();
            string newAccessToken = GenerateAccessToken(user);
            string refreshToken = GenerateRefreshToken();
            response.Code = 200;
            response.Data = new
            {
                refreshToken = refreshToken,
                accessToken = newAccessToken
            };
            return response;
        }
        public async Task<BaseResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var response = new BaseResponse();
            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.accessedToken);

            if (principal != null)
            {
                var userName = principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name);
                var user = await _userService.GetUser(userName?.Value);

                if (user is null || user.RefreshToken != refreshTokenRequest.refreshToken)
                {
                    response.Code = 201;
                    response.Message = "无效的Token";
                    return response;
                }

                string newAccessToken = GenerateAccessToken(user);
                string refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                await _userService.UpdateAsync(user);

                response.Code = 200;
                response.Data = new
                {
                    refreshToken = refreshToken,
                    accessToken = newAccessToken
                };

                return response;
            }

            return response;
        }
        public string GenerateAccessToken(SysUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email, user.EMail)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWT:DurationInMinutes"])),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateRefreshToken()
        {

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyDetail)
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
