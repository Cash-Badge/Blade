using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's '/item/access_token/update_version' endpoint. If you have an access_token from the legacy version of Plaid’s API, you can use the '/item/access_token/update_version' endpoint to generate an access_token for the Item that works with the current API.
    /// </summary>
    /// <remarks>Calling this endpoint does not revoke the legacy API access_token. You can still use the legacy access_token in the legacy API environment to retrieve data. You can also begin using the new access_token with our current API immediately.</remarks>
    public class UpdateAccessTokenVersionResponse : Response
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Entity.Item"/> identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public string Item { get; set; }
    }
}