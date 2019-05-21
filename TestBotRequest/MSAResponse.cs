using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotRequest
{
    class MSAResponse
    {
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
        public int Ext_Expires_In { get; set; }
        public string Access_Token { get; set; }
    }
}
