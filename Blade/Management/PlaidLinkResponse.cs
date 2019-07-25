using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's drop-in authentication module.
    /// </summary>
    public class PlaidLinkResponse
    {
        /// <summary>
        /// Gets or sets the public_token.
        /// </summary>
        /// <value>The public token.</value>
        public string PublicToken { get; set; }

        /// <summary>
        /// Gets or sets the link session identifier.
        /// </summary>
        /// <value>The link session identifier.</value>
        [JsonPropertyName("link_session_id")]
        public string LinkSession { get; set; }

        /// <summary>
        /// Gets or sets the accounts info.
        /// </summary>
        /// <value>The accounts.</value>
        public AccountInfo[] Accounts { get; set; }

        /// <summary>
        /// Gets or sets the institution.
        /// </summary>
        /// <value>The institution.</value>
        public InstitutionInfo Institution { get; set; }

        /// <summary>
        /// Represents an <see cref="Entity.Account"/> metadata.
        /// </summary>
        public struct AccountInfo
        {
            /// <summary>
            /// Gets or sets the <see cref="Entity.Account"/> identifier.
            /// </summary>
            /// <value>The identifier.</value>
            [JsonPropertyName("id")]
            public string Identifier { get; set; }

            /// <summary>
            /// Gets or sets the <see cref="Entity.Account"/> name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
        }

        /// <summary>
        /// Represents an <see cref="Entity.Institution"/> metadata.
        /// </summary>
        public struct InstitutionInfo
        {
            /// <summary>
            /// Gets or sets the <see cref="Entity.Institution"/> identifier.
            /// </summary>
            /// <value>The identifier.</value>
            [JsonPropertyName("institution_id")]
            public string Identifier { get; set; }

            /// <summary>
            /// Gets or sets the <see cref="Entity.Institution"/> name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
        }
    }
}