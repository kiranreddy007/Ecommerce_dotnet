using System;
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

        // Static constructor to load environment variables and perform initialization
        static JwtHelper()
        {
            // Load the .env file
            Env.Load();

            // Retrieve environment variables
            SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET") 
                        ?? throw new Exception("JWT_SECRET is not set in the environment.");
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "yourapp";
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "yourapp";

            // Log the loaded values for debugging (DO NOT log the actual secret in production)
            Console.WriteLine($"[JwtHelper] JWT_SECRET Length: {SecretKey.Length}");
            Console.WriteLine($"[JwtHelper] JWT_ISSUER: {Issuer}");
            Console.WriteLine($"[JwtHelper] JWT_AUDIENCE: {Audience}");

            // Ensure the secret key is long enough
            if (SecretKey.Length < 32)
            {
                throw new Exception("JWT_SECRET must be at least 32 characters long.");
            }
        }

        public static string GenerateJwtToken(string username, string role)
        {
            Console.WriteLine($"[JwtHelper] Generating token for user: {username}, role: {role}");

            // Define claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate the JWT token
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            Console.WriteLine("[JwtHelper] Token generated successfully.");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}