﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TestBotRequest
{
    public class ConversationAccount
    {

        public ConversationAccount(bool? isGroup = default(bool?), string conversationType = default(string), string id = default(string), string name = default(string), string aadObjectId = default(string), string role = default(string), string tenantId = default(string))
        {
            IsGroup = isGroup;
            ConversationType = conversationType;
            Id = id;
            Name = name;
            AadObjectId = aadObjectId;
            Role = role;
            TenantId = tenantId;
            //CustomInit();
        }

        /// <summary>
        /// Gets or sets indicates whether the conversation contains more than
        /// two participants at the time the activity was generated
        /// </summary>
        [JsonProperty(PropertyName = "isGroup")]
        public bool? IsGroup { get; set; }

        /// <summary>
        /// Gets or sets indicates the type of the conversation in channels
        /// that distinguish between conversation types
        /// </summary>
        [JsonProperty(PropertyName = "conversationType")]
        public string ConversationType { get; set; }

        /// <summary>
        /// Gets or sets channel id for the user or bot on this channel
        /// (Example: joe@smith.com, or @joesmith or 123456)
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets display friendly name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets this account's object ID within Azure Active Directory
        /// (AAD)
        /// </summary>
        [JsonProperty(PropertyName = "aadObjectId")]
        public string AadObjectId { get; set; }

        /// <summary>
        /// Gets or sets role of the entity behind the account (Example: User,
        /// Bot, etc.). Possible values include: 'user', 'bot'
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets this conversation's tenant ID
        /// </summary>
        [JsonProperty(PropertyName = "tenantId")]
        public string TenantId { get; set; }
    }
}
