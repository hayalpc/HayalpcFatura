using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hayalpc.Fatura.Panel.Internal.Helpers
{
    public class TokenCreator : ITokenCreator
    {
        private bool RememberMe { get; set; } = false;
        private LoginRequest LoginRequest { get; set; } = null;
        private UserDto User { get; set; } = null;

        public string CreateToken(LoginRequest loginRequest)
        {
            RememberMe = loginRequest.RememberMe;
            LoginRequest = loginRequest;
            return CreateToken();
        }

        public string CreateToken(LoginRequest loginRequest, UserDto user)
        {
            RememberMe = loginRequest.RememberMe;
            User = user;
            LoginRequest = loginRequest;
            return CreateToken();
        }

        public string CreateToken()
        {
            var token = CreateSecurityToken();

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken CreateSecurityToken()
        {
            var token = new JwtSecurityToken(
               issuer: AppConfigHelper.JwtIssuer,
               audience: AppConfigHelper.JwtAudience,
               expires: DateTime.Now.AddMinutes((RememberMe ? 1 : 10) * 60),
               signingCredentials: CreateCredentials(),
               claims: CreateClaims()
             );
            return token;
        }

        public SigningCredentials CreateCredentials()
        {
            var secretKey = AppConfigHelper.JwtSecurityKey;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return creds;
        }

        private List<Claim> CreateClaims()
        {
            var claims = new List<Claim>();
            if (User != null)
            {
                claims.Add(new Claim(ClaimTypes.Sid, LoginRequest.SessionId));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()));
            }
            return claims;
        }
    }
}
