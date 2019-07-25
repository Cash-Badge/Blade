using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents a Plaid item. A set of credentials at a financial institution.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("item_id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the available products.
        /// </summary>
        /// <value>The available products.</value>
        public string[] AvailableProducts { get; set; }

        /// <summary>
        /// Gets or sets the billed products.
        /// </summary>
        /// <value>The billed products.</value>
        public string[] BilledProducts { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Institution"/> identifier.
        /// </summary>
        /// <value>The institution identifier.</value>
        public string InstitutionId { get; set; }

        /// <summary>
        /// Gets or sets the webhook.
        /// </summary>
        /// <value>The webhook.</value>
        public string Webhook { get; set; }
    }
}