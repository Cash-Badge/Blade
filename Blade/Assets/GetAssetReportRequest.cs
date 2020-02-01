using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class GetAssetReportRequest
    {
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        public string Secret { get; set; }

        [JsonPropertyName("asset_report_token")]
        public string Token { get; set; }

        public bool IncludeInsights { get; set; }
    }
}
