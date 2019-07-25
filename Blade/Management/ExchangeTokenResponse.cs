using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's '/item/public_token/exchange' endpoint. Exchange a Link public_token for an API access_token.
    /// </summary>
    /// <seealso cref="Response" />
    public class ExchangeTokenResponse : Response
    {
        /// <summary>
        /// Gets or sets the <see cref="Entity.Item"/> identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }
    }
}