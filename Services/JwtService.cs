using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tryitter.Dtos;
using Tryitter.Models;

namespace Tryitter.Services
{
    public class JwtService
    {
        public LoginResponseDto CreateJwt(User user)
        {
            
            var identityClaims = GetUserClaims(new List<Claim>(), user);
            var encodedToken = CreateToken(identityClaims);

            return GetResponseToken(encodedToken, user);
        }

        private LoginResponseDto GetResponseToken(string encodedToken, User user)
        {
            return new LoginResponseDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(24).TotalSeconds,
                
            };
        }

        private ClaimsIdentity GetUserClaims(ICollection<Claim> claims, User user)
        {

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CreateToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ddsfdgfvbc\vgfsghdfnbfgnhgjgs");
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "localhost",
                Audience = "localhost",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}

