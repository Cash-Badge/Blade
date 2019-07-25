using System.Text.Json.Serialization;

namespace Blade
{
    /// <summary>
    /// Provides methods and properties for making a standard request.
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the client_id.
        /// </summary>
        /// <value>The client identifier.</value>
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        /// <summary>
        /// Gets or sets the access_token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }
    }
}