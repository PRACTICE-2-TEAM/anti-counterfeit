using Newtonsoft.Json;

namespace Anticontrafact2.Api
{
    /// <summary>
    /// Результат регистрации.
    /// </summary>
    public class RegistrationInfo
    {
        /// <summary>
        /// Успешно ли прошла операция.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Причина, по которой операция провалилась.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
