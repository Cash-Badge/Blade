
using System;
using System.Text.Json.Serialization;

namespace Blade.Transactions
{
    /// <summary>
    /// Represents a request for plaid's '/transactions/get' endpoint. The '/transactions/get' endpoint allows developers to receive user-authorized transaction data for credit and depository-type Accounts. Transaction data is standardized across financial institutions, and in many cases transactions are linked to a clean name, entity type, location, and category. Similarly, account data is standardized and returned with a clean name, number, balance, and other meta information where available.
    /// </summary>
    /// <remarks>Due to the potentially large number of transactions associated with an <see cref="Entity.Item"/>, results are paginated. Manipulate the count and offset parameters in conjunction with the total_transactions response body field to fetch all available Transactions.</remarks>
    public class GetTransactionsRequest : Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionsRequest"/> class.
        /// </summary>
        public GetTransactionsRequest()
        {
            EndDate = DateTime.Now;
            StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(30));
        }

        [JsonPropertyName("start_date")]
        public string StartDateString => StartDate.ToShortDateString();

        [JsonPropertyName("end_date")]
        public string EndDateString => EndDate.ToShortDateString();

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [JsonIgnore, JsonPropertyName("ignore_a")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [JsonIgnore, JsonPropertyName("ignore_b")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the pagination options.
        /// </summary>
        /// <value>The pagination options.</value>
        public PaginationOptions Options { get; set; }

        /// <summary>
        /// Represents pagination options.
        /// </summary>
        public class PaginationOptions
        {
            /// <summary>
            /// Gets or sets the number of transactions to fetch, where 0 < count <= 500.
            /// </summary>
            /// <value>The number of transactions to return.</value>
            [JsonPropertyName("count")]
            public uint Total
            {
                get => Count;
                set => Count = value < 1 ? 1 : value > 500 ? 500 : value;
            }

            /// <summary>
            /// Gets or sets the number of transactions to skip, where offset &gt;= 0.
            /// </summary>
            /// <value>The offset.</value>
            public uint Offset { get; set; }

            /// <summary>
            /// Gets or sets the list of account ids to retrieve for the <see cref="Entity.Item" />. Note: An error will be returned if a provided account_id is not associated with the <see cref="Entity.Item" />.
            /// </summary>
            /// <value>The account ids.</value>
            public string[] AccountIds { get; set; }

            [JsonIgnore]
            uint Count { get; set; } = 100;
        }
    }
}