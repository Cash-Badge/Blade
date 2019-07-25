using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents a banking institution.
    /// </summary>
    public class Institution
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("institution_id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has Multi-Factor Authentication.
        /// </summary>
        /// <value><c>true</c> if this instance has Multi-Factor Authentication; otherwise, <c>false</c>.</value>
        public bool HasMfa { get; set; }

        /// <summary>
        /// Gets or sets the Multi-Factor Authentication selections.
        /// </summary>
        /// <value>The mfa selections.</value>
        [JsonPropertyName("mfa")]
        public string[] MfaSelections { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public Credential[] Credentials { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public string[] Products { get; set; }

        /// <summary>
        /// Represents an <see cref="Institution"/> login credentials.
        /// </summary>
        public struct Credential
        {
            /// <summary>
            /// Gets or sets the label.
            /// </summary>
            /// <value>The label.</value>
            public string Label { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the type of the data.
            /// </summary>
            /// <value>The type of the data.</value>
            [JsonPropertyName("type")]
            public string DataType { get; set; }
        }
    }
}