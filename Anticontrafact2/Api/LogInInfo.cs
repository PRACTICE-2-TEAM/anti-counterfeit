using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    /// <summary>
    /// Результат авторизации.
    /// </summary>
    public class LogInInfo
    {
        /// <summary>
        /// Токен.
        /// Если авторизация не удалась хранит пустую строку.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
