using OneKey.Shared.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace OneKey.Shared.Utilities;

public class IdentityResolver : IIdentityResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CurrentUser> GetCurrentAccountAsync()
    {
        var currentUser = new CurrentUser();

        var firstName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        var lastName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "LastName")?.Value;
        var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "UserName")?.Value;
        var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value;
        var reference = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "Reference")?.Value;

        currentUser.FirstName = firstName;
        currentUser.LastName = lastName;
        currentUser.Username = username;
        currentUser.Email = email;
        currentUser.Reference = reference;
        currentUser.IsSignedIn = !string.IsNullOrWhiteSpace(currentUser.Reference);
        
        return currentUser;
    }
}

public interface IIdentityResolver
{
    public Task<CurrentUser> GetCurrentAccountAsync();
}