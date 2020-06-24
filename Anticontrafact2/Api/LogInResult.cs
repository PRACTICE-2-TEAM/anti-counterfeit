using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class LogInResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
