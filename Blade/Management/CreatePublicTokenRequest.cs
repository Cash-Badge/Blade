using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a request to plaid's '/item/public_token/create' endpoint, which allows for the generation of an <see cref="Management"/> access_token via <see cref="PlaidClient.ExchangeTokenAsync(ExchangeTokenRequest)"/>.
    /// </summary>
    /// <remarks>This request can only be used in the <see cref="Environment.Sandbox"/> environment.</remarks>
    public class CreatePublicTokenRequest
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The token that can be used to access and refer to a target <see cref="Entity.Item"/> on Plaid's servers.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
