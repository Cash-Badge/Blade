using Blade.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Assets.Entities
{
    public class Configuration
    {
        [JsonPropertyName("client_report_id")]
        public string Identifier { get; set; }

        public string Webhook { get; set; }

        public User User { get; set; }
    }
}
