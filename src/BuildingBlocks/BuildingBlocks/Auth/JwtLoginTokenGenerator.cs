using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BuildingBlocks.Auth;

public record AccessTokenResult(string Value, TimeSpan ExpiresIn);

public interface ILoginTokenGenerator
{
    AccessTokenResult GenerateAccessToken(Guid clienteId);
    string GenerateRefreshToken();
}

public class JwtLoginTokenGenerator(IConfiguration config) : ILoginTokenGenerator
{
    private readonly IConfigurationSection _jwtSettings = config.GetSection("Jwt");

    public AccessTokenResult GenerateAccessToken(Guid clienteId)
    {
        var secret = _jwtSettings["Secret"]!;
        var issuer = _jwtSettings["Issuer"];
        var audience = _jwtSettings["Audience"];
        var expiresIn = TimeSpan.FromMinutes(
            _jwtSettings.GetValue<double?>("AccessTokenExpirationMinutes") ?? 15);

        var claims = new[]
        {
            new Claim("clienteId", clienteId.ToString()),
            new Claim("purpose", "login")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiresIn),
            signingCredentials: credentials);

        var value = new JwtSecurityTokenHandler().WriteToken(token);
        return new AccessTokenResult(value, expiresIn);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }
}