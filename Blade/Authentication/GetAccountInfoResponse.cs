using System.Text.Json.Serialization;

namespace Blade.Authentication
{
    /// <summary>
    /// Represents a response from plaid's '/auth/get' endpoint. The /auth/get endpoint allows you to retrieve the bank account and routing numbers associated with an <see cref="Entity.Item"/>’s checking and savings accounts, along with high-level account data and balances.
    /// </summary>
    /// <seealso cref="Response" />
    public class GetAccountInfoResponse : Response
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Entity.Item Item { get; set; }

        /// <summary>
        /// Gets or sets the bank accounts.
        /// </summary>
        /// <value>The accounts.</value>
        public Entity.Account[] Accounts { get; set; }

        /// <summary>
        /// Gets or sets the  routing and account numbers.
        /// </summary>
        /// <value>The numbers.</value>
        public AccountIdentifiers Numbers { get; set; }

        /// <summary>
        /// Seperates account types into ACH and EFT types.
        /// </summary>
        public struct AccountIdentifiers
        {
            /// <summary>
            /// Gets or sets an array of ACH account numbers.
            /// </summary>
            /// <value>The array of ACH numbers.</value>
            [JsonPropertyName("ach")]
            public AchAccountNumbers[] ACH { get; set; }

            /// <summary>
            /// Gets or sets an array of EFT account numbers.
            /// </summary>
            /// <value>The array of EFT numbers.</value>
            [JsonPropertyName("eft")]
            public EtfAccountNumbers[] EFT { get; set; }
        }

        /// <summary>
        /// Represents the bank account and routing numbers associated with an ACH account on a item.
        /// </summary>
        public struct AchAccountNumbers
        {
            /// <summary>
            /// Gets or sets the plaid account identifier.
            /// </summary>
            /// <value>The account identifier.</value>
            public string AccountId { get; set; }

            /// <summary>
            /// Gets or sets the account number.
            /// </summary>
            /// <value>The account number.</value>
            [JsonPropertyName("account")]
            public string AccountNumber { get; set; }

            /// <summary>
            /// Gets or sets the routing number.
            /// </summary>
            /// <value>The routing number.</value>
            [JsonPropertyName("routing")]
            public string RoutingNumber { get; set; }

            /// <summary>
            /// Gets or sets the wire routing number.
            /// </summary>
            /// <value>The wire routing number.</value>
            [JsonPropertyName("wire_routing")]
            public string WireRoutingNumber { get; set; }
        }

        /// <summary>
        /// Represents the bank account and institution/branch associated with an EFT account on a item.
        /// </summary>
        public struct EtfAccountNumbers
        {
            /// <summary>
            /// Gets or sets the Plaid account ID associated with the account numbers.
            /// </summary>
            /// <value>The account identifier.</value>
            public string AccountId { get; set; }

            /// <summary>
            /// Gets or sets the EFT account number for the account.
            /// </summary>
            /// <value>The account number.</value>
            [JsonPropertyName("account")]
            public string AccountNumber { get; set; }

            /// <summary>
            /// Gets or sets the EFT institution number for the account.
            /// </summary>
            /// <value>The institution identifier.</value>
            public string Institution { get; set; }

            /// <summary>
            /// Gets or sets the EFT branch number for the account.
            /// </summary>
            /// <value>The branch number.</value>
            public string Branch { get; set; }
        }
    }
}