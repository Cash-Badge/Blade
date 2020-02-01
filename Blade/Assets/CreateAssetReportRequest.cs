using Blade.Assets.Entities;
using Blade.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets
{
    public class CreateAssetReportRequest
    {
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        public string Secret { get; set; }

        public List<string> AccessTokens { get; set; }

        public int? DaysRequested { get; set; }

        public Configuration Options { get; set; }
    }
}
