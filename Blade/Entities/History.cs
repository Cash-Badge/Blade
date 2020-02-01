using System;
using System.Text.Json.Serialization;

namespace Blade.Entity
{
    public class History
    {
        [JsonPropertyName("current")]
        public string Value { get; set; }

        public DateTime Date { get; set; }

        [JsonPropertyName("iso_currency_code")]
        public string CurrencyCode { get; set; }

        public string UnofficialCurrencyCode { get; set; }
    }
}
