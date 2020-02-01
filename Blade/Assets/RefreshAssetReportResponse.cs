using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class RefreshAssetReportResponse : Response
    {
        [JsonPropertyName("asset_report_id")]
        public string Reference { get; set; }

        [JsonPropertyName("asset_report_token")]
        public string Token { get; set; }
    }
}
