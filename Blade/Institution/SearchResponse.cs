namespace Blade.Institution
{
    /// <summary>
    /// Represents a request for plaid's '/institutions/search' endpoints. The '/institutions/search' endpoint to retrieve a complete list of supported institutions that match the query.
    /// </summary>
    /// <seealso cref="Response" />
    public class SearchResponse : Response
    {
        /// <summary>
        /// Gets or sets the institutions.
        /// </summary>
        /// <value>The institutions.</value>
        public Entity.Institution[] Institutions { get; set; }
    }
}