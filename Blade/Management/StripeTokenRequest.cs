using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a request for plaid's '/processor/stripe/bank_account_token/create' endpoint. Exchange a Link access_token for an Stripe API stripe_bank_account_token and request_id. 
    /// </summary>
    public class StripeTokenRequest
    {
        /// <summary>
        /// Gets or sets the access_token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
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
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The account id.</value>
        [JsonPropertyName("account_id")]
        public string Account { get; set; }
    }
}
