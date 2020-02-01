using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Entity
{
    public class User
    {
        [JsonPropertyName("client_user_id")]
        public string Identifier { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [JsonPropertyName("ssn")]
        public string SSN { get; set; }

        [JsonPropertyName("phone_number")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string EMail { get; set; }
    }
}
