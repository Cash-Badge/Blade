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
        /// Gets or sets the <see cref="Entity.Institution"/> identifier.
        /// </summary>
        /// <value>The institution identifier.</value>
        [JsonPropertyName("institution_id")]
        public string Institution { get; set; }

        /// <summary>
        /// An object with information about the error status of the Item. If there is no error, all values will be null.
        /// </summary>
        public ItemError Error { get; set; }

        /// <summary>
        /// Gets or sets the webhook.
        /// </summary>
        /// <value>The webhook.</value>
        public string Webhook { get; set; }

        public class ItemError
        {
            /// <summary>
            /// A broad categorization of the error.
            /// </summary>
            public string ErrorType { get; set; }

            [JsonIgnore]
            public ItemErrorType StrongErrorType => ErrorType.ReverseGenerateEnumValue(ItemErrorType.None);

            /// <summary>
            /// The particular error code. Each <see cref="ItemErrorType"/> has a specific set of possible <see cref="ErrorCode"/> values, except for <see cref="ItemErrorType.None"/>.
            /// </summary>
            public string ErrorCode { get; set; }

            /// <summary>
            /// A developer-friendly representation of the error code.
            /// </summary>
            public string ErrorMessage { get; set; }

            /// <summary>
            /// A user-friendly representation of the error code. null if the error is not related to user action.
            /// </summary>
            public string DisplayMessage { get; set; }

            public enum ItemErrorType
            {
                /// <summary>
                /// No error type
                /// </summary>
                None,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an invalid request.
                /// </summary>
                InvalidRequest,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an error caused by invalid user input.
                /// </summary>
                InvalidInput,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an error caused by the target institution.
                /// </summary>
                InstitutionError,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an error caused by exceeding the set rate limit.
                /// </summary>
                RateLimitExceeded,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an error caused by the target API endpoint.
                /// </summary>
                ApiError,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an item error.
                /// </summary>
                ItemError,

                /// <summary>
                /// This <see cref="ItemError"/> instance describes an asset report error.
                /// </summary>
                AssetReportError
            }
        }
    }
}