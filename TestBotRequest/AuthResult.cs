using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotRequest
{
    class AuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserUniqueId { get; set; }
        public long ExpiresOnUtcTicks { get; set; }
        public byte[] TokenCache { get; set; }
        public string IdentityProvider { get; set; }
        public string UserName { get; set; }
        public string UserPrincipalName { get; set; }
        public string TenantId { get; set; }
    }
}
