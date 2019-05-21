using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;

namespace TestBotRequest
{
    public class Activity
    {

        public Activity CreateReply(string text = null, string locale = null)
        {
            var reply = new Activity
            {
                Type = "message",
                Timestamp = DateTime.UtcNow,
                From = new ChannelAccount(id: this.Recipient?.Id, name: this.Recipient?.Name),
                Recipient = new ChannelAccount(id: this.From.Id, name: this.From.Name),
                //ReplyToId = this.Id,
                Id=new Guid().ToString(),
                ServiceUrl = this.ServiceUrl,
                ChannelId = this.ChannelId,
                Conversation = new ConversationAccount(isGroup: this.Conversation.IsGroup, id: this.Conversation.Id, name: this.Conversation.Name),
                Text = text ?? string.Empty,
                Locale = locale ?? this.Locale,
                Attachments = new List<Attachment>(),
                Entities = new List<Entity>(),
            };
            return reply;
        }
        /// <summary>
        /// Gets or sets contains the activity type. Possible values include:
        /// 'message', 'contactRelationUpdate', 'conversationUpdate', 'typing',
        /// 'endOfConversation', 'event', 'invoke', 'deleteUserData',
        /// 'messageUpdate', 'messageDelete', 'installationUpdate',
        /// 'messageReaction', 'suggestion', 'trace', 'handoff'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets contains an ID that uniquely identifies the activity
        /// on the channel.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets contains the date and time that the message was sent,
        /// in UTC, expressed in ISO-8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// Gets or sets contains the date and time that the message was sent,
        /// in local time, expressed in ISO-8601 format.
        /// For example, 2016-09-23T13:07:49.4714686-07:00.
        /// </summary>
        [JsonProperty(PropertyName = "localTimestamp")]
        public System.DateTimeOffset? LocalTimestamp { get; set; }

        /// <summary>
        /// Gets or sets contains the name of the timezone in which the
        /// message, in local time, expressed in IANA Time Zone database
        /// format.
        /// For example, America/Los_Angeles.
        /// </summary>
        [JsonProperty(PropertyName = "localTimezone")]
        public string LocalTimezone { get; set; }

        /// <summary>
        /// Gets or sets contains the URL that specifies the channel's service
        /// endpoint. Set by the channel.
        /// </summary>
        [JsonProperty(PropertyName = "serviceUrl")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets contains an ID that uniquely identifies the channel.
        /// Set by the channel.
        /// </summary>
        [JsonProperty(PropertyName = "channelId")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets identifies the sender of the message.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public ChannelAccount From { get; set; }

        /// <summary>
        /// Gets or sets identifies the conversation to which the activity
        /// belongs.
        /// </summary>
        [JsonProperty(PropertyName = "conversation")]
        public ConversationAccount Conversation { get; set; }

        /// <summary>
        /// Gets or sets identifies the recipient of the message.
        /// </summary>
        [JsonProperty(PropertyName = "recipient")]
        public ChannelAccount Recipient { get; set; }

        /// <summary>
        /// Gets or sets format of text fields Default:markdown. Possible
        /// values include: 'markdown', 'plain', 'xml'
        /// </summary>
        [JsonProperty(PropertyName = "textFormat")]
        public string TextFormat { get; set; }

        /// <summary>
        /// Gets or sets the layout hint for multiple attachments. Default:
        /// list. Possible values include: 'list', 'carousel'
        /// </summary>
        [JsonProperty(PropertyName = "attachmentLayout")]
        public string AttachmentLayout { get; set; }

        /// <summary>
        /// Gets or sets the collection of members added to the conversation.
        /// </summary>
        [JsonProperty(PropertyName = "membersAdded")]
        public IList<ChannelAccount> MembersAdded { get; set; }

        /// <summary>
        /// Gets or sets the collection of members removed from the
        /// conversation.
        /// </summary>
        [JsonProperty(PropertyName = "membersRemoved")]
        public IList<ChannelAccount> MembersRemoved { get; set; }

        /// <summary>
        /// Gets or sets the collection of reactions added to the conversation.
        /// </summary>
        //[JsonProperty(PropertyName = "reactionsAdded")]
        //public IList<MessageReaction> ReactionsAdded { get; set; }

        /// <summary>
        /// Gets or sets the collection of reactions removed from the
        /// conversation.
        /// </summary>
        //[JsonProperty(PropertyName = "reactionsRemoved")]
        //public IList<MessageReaction> ReactionsRemoved { get; set; }

        /// <summary>
        /// Gets or sets the updated topic name of the conversation.
        /// </summary>
        [JsonProperty(PropertyName = "topicName")]
        public string TopicName { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the prior history of the channel is
        /// disclosed.
        /// </summary>
        [JsonProperty(PropertyName = "historyDisclosed")]
        public bool? HistoryDisclosed { get; set; }

        /// <summary>
        /// Gets or sets a locale name for the contents of the text field.
        /// The locale name is a combination of an ISO 639 two- or three-letter
        /// culture code associated with a language
        /// and an ISO 3166 two-letter subculture code associated with a
        /// country or region.
        /// The locale name can also correspond to a valid BCP-47 language tag.
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the text content of the message.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the text to speak.
        /// </summary>
        [JsonProperty(PropertyName = "speak")]
        public string Speak { get; set; }

        /// <summary>
        /// Gets or sets indicates whether your bot is accepting,
        /// expecting, or ignoring user input after the message is delivered to
        /// the client. Possible values include: 'acceptingInput',
        /// 'ignoringInput', 'expectingInput'
        /// </summary>
        [JsonProperty(PropertyName = "inputHint")]
        public string InputHint { get; set; }

        /// <summary>
        /// Gets or sets the text to display if the channel cannot render
        /// cards.
        /// </summary>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the suggested actions for the activity.
        /// </summary>
        //[JsonProperty(PropertyName = "suggestedActions")]
        //public SuggestedActions SuggestedActions { get; set; }

        /// <summary>
        /// Gets or sets attachments
        /// </summary>
        [JsonProperty(PropertyName = "attachments")]
        public IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets represents the entities that were mentioned in the
        /// message.
        /// </summary>
        [JsonProperty(PropertyName = "entities")]
        public IList<Entity> Entities { get; set; }

        /// <summary>
        /// Gets or sets contains channel-specific content.
        /// </summary>
        [JsonProperty(PropertyName = "channelData")]
        public object ChannelData { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the recipient of a
        /// contactRelationUpdate was added or removed from the sender's
        /// contact list.
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets contains the ID of the message to which this message
        /// is a reply.
        /// </summary>
        [JsonProperty(PropertyName = "replyToId")]
        public string ReplyToId { get; set; }

        /// <summary>
        /// Gets or sets a descriptive label for the activity.
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the type of the activity's value object.
        /// </summary>
        [JsonProperty(PropertyName = "valueType")]
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets a value that is associated with the activity.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation associated with an invoke or
        /// event activity.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a reference to another conversation or activity.
        /// </summary>
        //[JsonProperty(PropertyName = "relatesTo")]
        //public ConversationReference RelatesTo { get; set; }

        /// <summary>
        /// Gets or sets the a code for endOfConversation activities that
        /// indicates why the conversation ended. Possible values include:
        /// 'unknown', 'completedSuccessfully', 'userCancelled', 'botTimedOut',
        /// 'botIssuedInvalidMessage', 'channelFailed'
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the time at which the activity should be considered to
        /// be "expired" and should not be presented to the recipient.
        /// </summary>
        [JsonProperty(PropertyName = "expiration")]
        public System.DateTimeOffset? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the importance of the activity. Possible values
        /// include: 'low', 'normal', 'high'
        /// </summary>
        [JsonProperty(PropertyName = "importance")]
        public string Importance { get; set; }

        /// <summary>
        /// Gets or sets a delivery hint to signal to the recipient alternate
        /// delivery paths for the activity.
        /// The default delivery mode is "default". Possible values include:
        /// 'normal', 'notification'
        /// </summary>
        [JsonProperty(PropertyName = "deliveryMode")]
        public string DeliveryMode { get; set; }

        /// <summary>
        /// Gets or sets list of phrases and references that speech and
        /// language priming systems should listen for
        /// </summary>
        [JsonProperty(PropertyName = "listenFor")]
        public IList<string> ListenFor { get; set; }

        /// <summary>
        /// Gets or sets the collection of text fragments to highlight when the
        /// activity contains a ReplyToId value.
        /// </summary>
        //[JsonProperty(PropertyName = "textHighlights")]
        //public IList<TextHighlight> TextHighlights { get; set; }

        /// <summary>
        /// Gets or sets an optional programmatic action accompanying this
        /// request
        /// </summary>
        //[JsonProperty(PropertyName = "semanticAction")]
        //public SemanticAction SemanticAction { get; set; }
    }
}
