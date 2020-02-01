using System;
using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents a bank account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("account_id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Report.Item"/> identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last four digits of the Account's number.
        /// </summary>
        /// <value>The mask.</value>
        public string Mask { get; set; }

        /// <summary>
        /// Gets or sets the official name of the account.
        /// </summary>
        /// <value>The official name of the account.</value>
        public string OfficialName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        [JsonIgnore]
        public AccountType StrongType => Enum.Parse<AccountType>(Type, true);

        /// <summary>
        /// Gets or sets the type of the sub.
        /// </summary>
        /// <value>The type of the sub.</value>
        public string Subtype { get; set; }

        /// <summary>
        /// Gets or sets the balance. The current balance is the total amount of funds in the account. The available balance is the current balance less any outstanding holds or debits that have not yet posted to the account.
        /// </summary>
        /// <remarks>Note: Not all institutions calculate the available balance. In the event that available balance is unavailable from the institution, Plaid will return an available balance value of <c>null</c>.</remarks>
        /// <value>The balance.</value>
        [JsonPropertyName("balances")]
        public Balance Balance { get; set; }

        /// <summary>
        /// The current verification status of an Auth Item initiated through Automated or Manual microdeposits.
        /// </summary>
        public string VerificationStatus { get; set; }

        /// <summary>
        /// The identities of the owners of this account.
        /// </summary>
        public Identity[] Owners { get; set; }

        [JsonIgnore]
        public AccountVerificationStatus StrongVerificationStatus => VerificationStatus.ReverseGenerateEnumValue(AccountVerificationStatus.None);

        /// <summary>
        /// <see cref="VerificationStatus"/> possible states.
        /// </summary>
        public enum AccountVerificationStatus
        {
            /// <summary>
            /// No information on account verification is present
            /// </summary>
            None,

            /// <summary>
            /// An Item is pending automated microdeposit verfication
            /// </summary>
            PendingAutomaticVerification,
            
            /// <summary>
            /// An Item was successfully automatically verified
            /// </summary>
            AutomaticallyVerified,
            
            /// <summary>
            /// An Item is pending manual microdeposit verfication
            /// </summary>
            PendingManualVerification,
            
            /// <summary>
            /// An Item was successfully manually verified
            /// </summary>
            ManuallyVerified
        }

        /// <summary>
        /// Enum AccountType
        /// </summary>
        public enum AccountType
        {
            /// <summary>
            /// Brokerage account.
            /// </summary>
            Investment,

            /// <summary>
            /// Credit card.
            /// </summary>
            Credit,

            /// <summary>
            /// Checking or Savings accounts.
            /// </summary>
            Depository,

            /// <summary>
            /// Loan account.
            /// </summary>
            Loan,

            /// <summary>
            /// Non-specified account type
            /// </summary>
            Other
        }
    }
}