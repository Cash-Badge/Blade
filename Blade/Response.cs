using System.Net;
using System.Text.Json.Serialization;

namespace Blade
{
    /// <summary>
    /// Provides common members for all Plaid API responses.
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// Gets or sets the error that was returned.
        /// </summary>
        [JsonIgnore]
        public Exceptions.PlaidException Exception { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>The request identifier.</value>
        [JsonPropertyName("request_id")]
        public string Request { get; set; }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        /// <value>The status code.</value>
        [JsonIgnore]
        public HttpStatusCode Status { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is success status code.
        /// </summary>
        /// <value><c>true</c> if this instance is success status code; otherwise, <c>false</c>.</value>
        public bool SuccessfulOutcome => Status == HttpStatusCode.OK;
    }
}