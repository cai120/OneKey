using OneKey.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OneKey.Shared.Utilities;

public class TokenResolver : ITokenResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TokenResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetTokenAsync(string audience = null)
    {
        var signingCredentials = new SigningCredentials(OneKeyApiConstants.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: OneKeyWebConstants.OneKeyWebIssuer,
            audience: !string.IsNullOrWhiteSpace(audience) ? audience : OneKeyApiConstants.OneKeyApiAudience,
            signingCredentials: signingCredentials,
            claims: _httpContextAccessor.HttpContext?.User.Claims.ToList()
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GetTokenAsync(string audience = null, ClaimsPrincipal claims = null)
    {
        var signingCredentials = new SigningCredentials(OneKeyApiConstants.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: OneKeyWebConstants.OneKeyWebIssuer,
            audience: !string.IsNullOrWhiteSpace(audience) ? audience : OneKeyApiConstants.OneKeyApiAudience,
            signingCredentials: signingCredentials,
            claims: claims.Claims != null ? claims.Claims : _httpContextAccessor.HttpContext?.User.Claims.ToList()
        );
        

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public interface ITokenResolver
{
    public Task<string> GetTokenAsync(string audience = null);
    public Task<string> GetTokenAsync(string audience = null, ClaimsPrincipal claims = null);
}

