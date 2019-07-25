using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a request for plaid's '/item/access_token/update_version' endpoint. If you have an access_token from the legacy version of Plaid’s API, you can use the '/item/access_token/update_version' endpoint to generate an access_token for the Item that works with the current API.
    /// </summary>
    /// <remarks>Calling this endpoint does not revoke the legacy API access_token. You can still use the legacy access_token in the legacy API environment to retrieve data. You can also begin using the new access_token with our current API immediately.</remarks>
    public class UpdateAccessTokenVersionRequest
    {
        /// <summary>
        /// Gets or sets the client_id.
        /// </summary>
        /// <value>The client identifier.</value>
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the access token v1.
        /// </summary>
        /// <value>The access token v1.</value>
        public string AccessTokenV1 { get; set; }
    }
}