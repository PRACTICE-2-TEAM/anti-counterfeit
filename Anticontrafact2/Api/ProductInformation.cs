using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Anticontrafact2.Api
{
    public class ProductInformation
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("info")]
        public Information Info { get; set; }

        public class Information
        {
            [JsonProperty("country")]
            public string Country;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("brend")]
            public string Brand;

            [JsonProperty("unit_value")]
            public string UnitValue;

            [JsonProperty("article")]
            public string Article;
        }
    }

    
}
