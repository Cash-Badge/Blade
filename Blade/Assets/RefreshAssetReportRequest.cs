using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Blade.Assets.Entities;

namespace Blade.Assets
{
    public class RefreshAssetReportRequest
    {
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        public string Secret { get; set; }

        [JsonPropertyName("asset_report_token")]
        public string Token { get; set; }

        [JsonPropertyName("days_requested")]
        public int? Days { get; set; }

        public Configuration Options { get; set; }
    }
}
