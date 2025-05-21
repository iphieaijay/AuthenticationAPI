using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class TokenService: ITokenService
    {
        private readonly SymmetricSecurityKey _secretkey;
        private readonly string? _validIssuer, _validAudience;
        private readonly DateTime _expires;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TokenService> _logger;
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;
        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ILogger<TokenService> logger, IOptions<JwtSettings> jwtOptions)
        {
            _logger = logger;
            _config = configuration;
            _userManager = userManager; 
            _jwtSettings = jwtOptions.Value;
            
            _secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            _validIssuer = _jwtSettings.ValidIssuer;
            _validAudience = _jwtSettings.ValidAudience;
            _expires =DateTime.Now.AddMinutes(30);

        }
        public string GenerateRefreshToken()
        {
            var randNuum = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randNuum);
            var refreshToken = Convert.ToBase64String(randNuum);
            return refreshToken;

        }
        public (string token, DateTime expiresAt) GenerateToken(ApplicationUser user)
        {
            var signInCred = new SigningCredentials(_secretkey, SecurityAlgorithms.HmacSha256);
            var claims =  GetCliams(user);
            var tokenOptions = GenerateTokenOptions(signInCred, claims);
            return (new JwtSecurityTokenHandler().WriteToken(tokenOptions), _expires);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _validIssuer,
                audience: _validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );
          
          

        }

        private  List<Claim> GetCliams(ApplicationUser user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
           
            return claims;
        }
    }
}
