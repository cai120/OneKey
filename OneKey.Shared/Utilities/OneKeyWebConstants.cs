using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OneKey.Shared.Utilities
{
    public static class OneKeyWebConstants
    {
        public static string ClientScope { get => "OneKey-Web-Client"; }
        public static string SecurityKey { get; } = "eb7640d8-7985-4cb3-b673-b132cc750a67";

        public static SymmetricSecurityKey SymmetricSecurityKey { get; } = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

        public static string LoginEndpoint { get => ""; }
        public static string RegisterEndpoint { get => ""; }

        public static JwtSecurityToken Token { get; set; }

        public static string LocalHostUrl { get => ""; }
        public static string HostUrl { get; set; } = LocalHostUrl;
        public static string LiveUrl { get; set; }
        public static string OneKeyWebIssuer { get => "OneKey-Web"; }
    }
}