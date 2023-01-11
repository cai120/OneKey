using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OneKey.Shared.Utilities
{
    public static class OneKeyApiConstants
    {
        public static string Scope { get => "OneKey-Api"; }
        public static string SecurityKey { get; } = "eb7640d8-7985-4cb3-b673-b132cc750a67";
        public static SymmetricSecurityKey SymmetricSecurityKey { get; } = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        public static string TestHostUrl { get; set; } = "{put_url_here}";
        public static string HostUrl { get; set; } = TestHostUrl;
        public static string LiveUrl { get; set; } = "";
        public static string OneKeyApiAudience { get => "OneKey-Api"; }
    }
}