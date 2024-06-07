using System.Text.Json.Serialization;

namespace OceanCareChat.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int OceanUserId { get; set; }

        public int EventId { get; set; }

        [JsonIgnore]
        public OceanUser OceanUser { get; set; }
        [JsonIgnore]
        public Events Event { get; set; }
    }
}
