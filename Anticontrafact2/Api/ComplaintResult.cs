using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class ComplaintResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
