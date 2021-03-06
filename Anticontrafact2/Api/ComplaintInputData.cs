﻿using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    public class ComplaintInputData
    {
        [JsonProperty("textRequest")]
        public string Description { get; set; }

        [JsonProperty("adress")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
