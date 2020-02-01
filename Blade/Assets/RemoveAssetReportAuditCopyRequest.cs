using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class RemoveAssetReportAuditCopyRequest
    {
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        public string Secret { get; set; }

        [JsonPropertyName("audit_copy_token")]
        public string Token { get; set; }
    }
}
