using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class OutletInformation
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
