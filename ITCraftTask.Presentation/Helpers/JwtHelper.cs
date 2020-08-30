using ITCraftTask.BusinessLogicLayer.Constants;
using ITCraftTask.BusinessLogicLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ITCraftTask.Presentation.Helpers
{
    public class JwtHelper: IJwtHelper
    {
        private readonly SymmetricSecurityKey _secretKey;
        private readonly string _signingAlgorithm;
        private readonly DateTime _accessTime;
        private readonly DateTime _refreshTime;
        public SecurityKey GetKey() => _secretKey;

        public JwtHelper(IConfiguration configuration)
        {
        _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BusinessLogicLayer.Constants.JwtConstants.SigningSecurityKey));
            _signingAlgorithm = SecurityAlgorithms.HmacSha256;
            _accessTime = DateTime.Now.AddMinutes(5);
            _refreshTime = DateTime.Now.AddDays(60);
        }

        private List<Claim> GenerateClaims(long id)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };
            return claims;
        }

        private List<Claim> GenerateRefreshClaims(long id)
        {
            var claims = GenerateClaims(id);
            return claims;
        }

        private List<Claim> GenerateAccessClaims(UserModel model)
        {
            var claims = GenerateClaims(model.Id);
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(ClaimTypes.Name, model.Login.ToString()));


            return claims;
        }

        private string GenerateToken(List<Claim> claims, DateTime lifeTime)
        {
            var token = new JwtSecurityToken(
                issuer: BusinessLogicLayer.Constants.JwtConstants.ValidIssuer,
                audience: BusinessLogicLayer.Constants.JwtConstants.ValidAudience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: lifeTime,
                signingCredentials: new SigningCredentials(
                        GetKey(),
                        _signingAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        private string GenerateRefreshToken(UserModel model)
        {
            var claims = GenerateRefreshClaims(model.Id);
            return GenerateToken(claims, _refreshTime);
        }

        private string GenerateAccessToken(UserModel model)
        {
            var claims = GenerateAccessClaims(model);
            return GenerateToken(claims, _accessTime);
        }

        public TokensModel GenerateTokens(UserModel model)
        {
            var responce = new BaseModel();
            var accessToken = GenerateAccessToken(model);
            if (accessToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            var refreshToken = GenerateRefreshToken(model);
            if (refreshToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            return new TokensModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public TokensModel RefreshOldTokens(UserModel model, string refreshToken)
        {
            var responce = new BaseModel();
            var newRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            if (newRefreshToken.ValidTo <= DateTime.Now)
            {
                return GenerateTokens(model);
            }
            var accessToken = GenerateAccessToken(model);
            if (accessToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            return new TokensModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public long GetIdFromToken(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = jwt.Claims.Where(x => x.Type.Contains("nameidentifier")).FirstOrDefault().Value;
            var result = Int64.Parse(id);
            return result;
        }

        private ClaimsIdentity GetIdentity(UserModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
