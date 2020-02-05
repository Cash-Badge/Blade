using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Entity
{
    public partial class Report
    {
        [JsonPropertyName("asset_report_id")]
        public string Reference { get; set; }

        [JsonPropertyName("client_report_id")]
        public string Identifier { get; set; }

        [JsonPropertyName("date_generated")]
        public DateTime Generated { get; set; }

        [JsonPropertyName("days_request")]
        public int Days { get; set; }

        public List<Item> Items { get; set; }

        public User User { get; set; }

        public struct Item 
        { 
            public List<Account> Accounts { get; set; }

            [JsonPropertyName("date_last_updated")]
            public DateTime Update { get; set; }

            [JsonPropertyName("institution_id")]
            public string Institution { get; set; }

            public string InstitutionName { get; set; }

            [JsonPropertyName("item_id")]
            public string Identifier { get; set; }
        }

        public struct Account
        {
            [JsonPropertyName("account_id")]
            public string Identifier { get; set; }
            
            [JsonPropertyName("balances")]
            public Balance Balance { get; set; }

            [JsonPropertyName("days_available")]
            public int Days { get; set; }

            [JsonPropertyName("historical_balances")]
            public List<History> History { get; set; }

            public string Mask { get; set; }

            public string Name { get; set; }

            public string OfficialName { get; set; }

            public List<Identity> Owners { get; set; }

            public string Subtype { get; set; }

            public List<Transaction> Transactions { get; set; }

            public string Type { get; set; }
        }

        public struct Transaction
        { 
            [JsonPropertyName("account_id")]
            public string Account { get; set; }

            public double Amount { get; set; }

            public DateTime Date { get; set; }

            [JsonPropertyName("original_description")]
            public string Description { get; set; }

            public bool Pending { get; set; }

            [JsonPropertyName("transaction_id")]
            public string Identifier { get; set; }

            [JsonPropertyName("iso_currency_code")]
            public string CurrencyCode { get; set; }

            public string UnofficialCurrencyCode { get; set; }
        }
    }
}
