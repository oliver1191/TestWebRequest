using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBotRequest
{
    public class Entity
    {
        /// <summary>
        /// Gets or sets type of this entity (RFC 3987 IRI)
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
