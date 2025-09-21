using Bainah.Core.Interfaces;
using Bainah.Core.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Bainah.Infrastructure.Security;
public class JwtOptions { public string Secret { get; set; } = string.Empty; public int ExpiryMinutes { get; set; } = 60; public string Issuer { get; set; } = string.Empty; public string Audience { get; set; } = string.Empty; }
public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    public JwtProvider(IOptions<JwtOptions> options) => _options = options.Value;
    public string GenerateToken(User user, IEnumerable<string> roles)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Name, user.UserName ?? ""), new Claim(ClaimTypes.Email, user.Email ?? "") };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _options.Issuer, audience: _options.Audience, claims: claims, expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
