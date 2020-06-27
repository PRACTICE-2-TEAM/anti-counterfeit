using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class ComplaintIdentifier
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
