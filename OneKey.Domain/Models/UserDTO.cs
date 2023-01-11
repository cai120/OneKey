using OneKey.Shared.Models;

namespace OneKey.Domain.Models;

public class UserDTO : BaseDTO
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
}
