using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class ProductInformation
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("info")]
        public string Information { get; set; }
    }
}
