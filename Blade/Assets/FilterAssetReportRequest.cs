using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class FilterAssetReportRequest
    {
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        public string Secret { get; set; }

        [JsonPropertyName("asset_report_token")]
        public string Token { get; set; }

        [JsonPropertyName("account_ids_to_exclude")]
        public List<string> Exclusions { get; set; }
    }
}
