using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace JicoDotNet.Authentication
{
    public class TokenManager
    {
        private readonly string _secretKey;

        public TokenManager(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(string username, int expireMinutes = 1500)
        {
            byte[] symmetricKey = Encoding.UTF8.GetBytes(_secretKey);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            DateTime now = DateTime.UtcNow.AddHours(5.5);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = now.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
            };
        }
    }

    public class UserAuthenticationService
    {
        private readonly TokenManager _tokenManager;

        public UserAuthenticationService(string secretKey)
        {
            _tokenManager = new TokenManager(secretKey);
        }

        public string Authenticate(string username)
        {
            // Replace with your own logic for validating the user's credentials
            return _tokenManager.GenerateToken(username);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            return _tokenManager.ValidateToken(token);
        }
    }

    public class KeyGenerator
    {
        public static string Generate256BitKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[32]; // 256 bits
                rng.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }
    }
}
