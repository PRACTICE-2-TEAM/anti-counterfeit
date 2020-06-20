using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    /// <summary>
    /// Результат проверки товара по штрих-коду.
    /// </summary>
    public class BarcodeInfo
    {
        /// <summary>
        /// Результат.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Информация о товаре.
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }
    }
}
