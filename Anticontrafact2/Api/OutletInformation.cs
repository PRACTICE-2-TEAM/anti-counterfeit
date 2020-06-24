using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class OutletInformation
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("info")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
