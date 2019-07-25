using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a request to plaid's '/sandbox/public_token/create' endpoint, which allows for the creation of an <see cref="Entity.Item"/> in the <see cref="Environment.Sandbox"/> environment, whoose public_token can then be exchanged for an access_token via <see cref="PlaidClient.ExchangeTokenAsync(ExchangeTokenRequest)"/>.
    /// </summary>
    /// <remarks>This request can only be used in the <see cref="Environment.Sandbox"/> environment.</remarks>
    public class CreateSandboxedPublicTokenRequest
    {
        /// <summary>
        /// The target <see cref="Entity.Institution"/> identifier.
        /// </summary>
        [JsonPropertyName("institution_id")]
        public string Institution { get; set; }

        /// <summary>
        /// The public key.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// The products to initially pull for the <see cref="Entity.Item"/>. May be any products that the specified <see cref="Institution"/> supports.
        /// </summary>
        public string[] InitialProducts { get; set; }

        /// <summary>
        /// The options for this request.
        /// </summary>
        /// <remarks>If provided, the value must be non-null.</remarks>
        public Settings Options { get; set; }

        /// <summary>
        /// Represents the settable options for the <see cref="CreateSandboxedPublicTokenRequest"/>.
        /// </summary>
        public class Settings
        {
            /// <summary>
            /// Specify a webhook to associate with the new Item.
            /// </summary>
            public string Webhook { get; set; }

            /// <summary>
            /// Test username to use for the creation of the Sandbox item.
            /// </summary>
            public string OverrideUsername { get; set; }

            /// <summary>
            /// Test password to use for the creation of the Sandbox Item.
            /// </summary>
            public string OverridePassword { get; set; }
        }
    }
}
