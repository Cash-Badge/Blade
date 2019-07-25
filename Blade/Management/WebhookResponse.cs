using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's webhook.
    /// </summary>
    public class WebhookResponse
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("webhook_type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [JsonPropertyName("webhook_code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Entity.Item"/> identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the new transactions.
        /// </summary>
        /// <value>The new transactions.</value>
        public int NewTransactions { get; set; }

        /// <summary>
        /// Gets or sets the removed transactions.
        /// </summary>
        /// <value>The removed transactions.</value>
        public string[] RemovedTransactions { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public ErrorDetails Error { get; set; }

        /// <summary>
        /// Gets or sets the new webhook.
        /// </summary>
        /// <value>The new webhook.</value>
        public string NewWebhook { get; set; }

        /// <summary>
        /// Represents an error.
        /// </summary>
        public struct ErrorDetails
        {
            /// <summary>
            /// Gets or sets the type of the error.
            /// </summary>
            /// <value>The type of the error.</value>
            public string ErrorType { get; set; }

            /// <summary>
            /// Gets or sets the error code.
            /// </summary>
            /// <value>The error code.</value>
            public string ErrorCode { get; set; }

            /// <summary>
            /// Gets or sets the display message.
            /// </summary>
            /// <value>The display message.</value>
            public string DisplayMessage { get; set; }

            /// <summary>
            /// Gets or sets the http status code.
            /// </summary>
            /// <value>The status.</value>
            public int Status { get; set; }
        }
    }
}