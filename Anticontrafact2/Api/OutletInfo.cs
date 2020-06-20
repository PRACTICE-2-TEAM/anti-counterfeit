using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    /// <summary>
    /// Результат проверки торговой точки по ИНН.
    /// </summary>
    public class OutletInfo
    {
        /// <summary>
        /// Результат.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [JsonProperty("info")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
