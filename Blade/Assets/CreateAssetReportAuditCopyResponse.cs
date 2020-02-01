using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class CreateAssetReportAuditCopyResponse : Response
    {
        [JsonPropertyName("audit_copy_token")]
        public string Token { get; set; }
    }
}
