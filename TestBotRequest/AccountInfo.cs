using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotRequest
{
    class AccountInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TenantId { get; set; }
        public string JobTitle { get; set; }
        public string Organization { get; set; }
        public string Country { get; set; }
        public string MySiteUrl { get; set; }
        public AuthResult AuthResult { get; set; }
        public AuthResult SPAuthResult { get; set; }
    }
}
