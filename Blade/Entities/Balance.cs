using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents an account balance.
    /// </summary>
    public struct Balance
    {
        /// <summary>
        /// Gets or sets the current balance.
        /// </summary>
        /// <value>The current.</value>
        public float Current { get; set; }

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>The available.</value>
        public float? Available { get; set; }

        /// <summary>
        /// Gets or sets the account limit.
        /// </summary>
        /// <value>The limit.</value>
        public float? Limit { get; set; }

        /// <summary>
        /// Gets or sets the ISO currency code.
        /// </summary>
        /// <value>currency code.</value>
        [JsonPropertyName("iso_currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the unofficial currency code.
        /// </summary>
        /// <value>currency code.</value>
        public string UnofficialCurrencyCode { get; set; }

    }
}