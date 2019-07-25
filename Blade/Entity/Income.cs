

using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents an user earnings.
    /// </summary>
    public class Income
    {
        /// <summary>
        /// Gets or sets the last year income.
        /// </summary>
        /// <value>The last year income.</value>
        public float LastYearIncome { get; set; }

        /// <summary>
        /// Gets or sets the last year income before tax.
        /// </summary>
        /// <value>The last year income before tax.</value>
        public float LastYearIncomeBeforeTax { get; set; }

        /// <summary>
        /// Gets or sets the projected yearly income.
        /// </summary>
        /// <value>The projected yearly income.</value>
        public float ProjectedYearlyIncome { get; set; }

        /// <summary>
        /// Gets or sets the projected yearly income before tax.
        /// </summary>
        /// <value>The projected yearly income before tax.</value>
        public float ProjectedYearlyIncomeBeforeTax { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of overlapping income streams.
        /// </summary>
        /// <value>The maximum number of overlapping income streams.</value>
        public int MaxNumberOfOverlappingIncomeStreams { get; set; }

        /// <summary>
        /// Gets or sets the total income streams.
        /// </summary>
        /// <value>The total income streams.</value>
        [JsonPropertyName("number_of_income_streams")]
        public int TotalIncomeStreams { get; set; }

        /// <summary>
        /// Gets or sets the icome streams.
        /// </summary>
        /// <value>The streams.</value>
        [JsonPropertyName("income_streams")]
        public Stream[] Streams { get; set; }

        /// <summary>
        /// Represents an income stream.
        /// </summary>
        public struct Stream
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the days.
            /// </summary>
            /// <value>The days.</value>
            public int Days { get; set; }

            /// <summary>
            /// Gets or sets the monthly income.
            /// </summary>
            /// <value>The monthly income.</value>
            public float MonthlyIncome { get; set; }

            /// <summary>
            /// Gets or sets the confidence.
            /// </summary>
            /// <value>The confidence.</value>
            public float Confidence { get; set; }
        }
    }
}