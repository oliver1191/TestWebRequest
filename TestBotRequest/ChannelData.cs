using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotRequest
{
    class ChannelData
    {
        [JsonProperty(PropertyName = "clientActivityID")]
        public string ClientActivityID { get; set; }
    }
}
