using System.Text.Json.Serialization;

namespace Blade.Institution
{
    /// <summary>
    /// Represents a request for plaid's '/institutions/get_by_id' endpoint. The '/institutions/get_by_id' endpoint to retrieve a <see cref="Entity.Institution"/> with the specified id.
    /// </summary>
    public class SearchByIdRequest
    {
        /// <summary>
        /// Gets or sets the <see cref="Entity.Institution"/> identifier.
        /// </summary>
        /// <value>The institution identifier.</value>
        [JsonPropertyName("institution_id")]
        public string Institution { get; set; }

        /// <summary>
        /// Gets or sets the public_key.
        /// </summary>
        /// <value>The public key.</value>
        public string PublicKey { get; set; }
    }
}