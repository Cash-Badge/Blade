using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents a transaction's category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("category_id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the hierarchy or sub-categories.
        /// </summary>
        /// <value>The hierarchy.</value>
        public string[] Hierarchy { get; set; }
    }
}