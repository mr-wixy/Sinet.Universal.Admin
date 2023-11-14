using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sinet.Universal.Admin.RCL.Global
{
    public class TokenInfo
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        public DateTime ExpireTime
        {
            get
            {
                return GetJwtTokenInfo().ValidTo.ToLocalTime();
            }
        }

        private JwtSecurityToken JwtTokenInfo { get; set; }

        private JwtSecurityToken JwtIdTokenInfo { get; set; }

        public JwtSecurityToken GetJwtTokenInfo()
        {
            JwtTokenInfo ??= new JwtSecurityToken(AccessToken);
            return JwtTokenInfo;
        }

        public JwtSecurityToken GetJwtIdTokenInfo()
        {
            JwtIdTokenInfo ??= new JwtSecurityToken(RefreshToken);
            return JwtIdTokenInfo;
        }
    }
}
