using Microsoft.AspNetCore.Identity;
using OneKey.Shared.Models;

namespace OneKey.Entity.Models;

public class User : IdentityUser<int>, IBaseEntity
{
    public string Reference { get; set; }
    public bool IsDeleted { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
