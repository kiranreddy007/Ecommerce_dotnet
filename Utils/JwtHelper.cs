using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

namespace EcommerceBackend.Utils
{
    public static class JwtHelper
    {
        private static readonly string SecretKey;
        private static readonly string Issuer;
        private static readonly string Audience;
        private static readonly int TokenExpirationHours;

        // Static constructor for initialization
        static JwtHelper()
        {
            // Load environment variables
            Env.Load();

            // Get required environment variables
            SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET") 
                        ?? throw new Exception("JWT_SECRET is not set in the environment.");
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "yourapp";
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "yourapp";
            TokenExpirationHours = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_HOURS") ?? "1");

            // Ensure the secret key is strong enough
            if (SecretKey.Length < 32)
            {
                throw new Exception("JWT_SECRET must be at least 32 characters long.");
            }
        }

        public static string GenerateJwtToken(string username, string role, int userId, Dictionary<string, string>? additionalClaims = null)
        {
            // Log token generation start (replace with a proper logger in production)
            Console.WriteLine($"[JwtHelper] Generating token for user: {username}, role: {role}, userId: {userId}");

            // Define basic claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("Id", userId.ToString()) // Custom claim for User ID
            };

            // Add any additional claims provided
            if (additionalClaims != null)
            {
                foreach (var claim in additionalClaims)
                {
                    claims.Add(new Claim(claim.Key, claim.Value));
                }
            }

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TokenExpirationHours), // Configurable expiration time
                NotBefore = DateTime.UtcNow, // Optional: Prevent token use before current time
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = creds
            };

            // Generate and return the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine("[JwtHelper] Token generated successfully.");
            return tokenHandler.WriteToken(token);
        }
    }
}