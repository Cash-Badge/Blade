namespace Blade
{
    /// <summary>
    /// An enumeration representing which Plaid API environment should be targeted.
    /// </summary>
    public enum Environment
    {
        /// <summary>
        /// A configuration for the <see cref="PlaidClient"/> instance to use the Plaid API server's "production" subdomain.
        /// </summary>
        Production,

        /// <summary>
        /// A configuration for the <see cref="PlaidClient"/> instance to use the Plaid API server's "development" subdomain.
        /// </summary>
        Development,

        /// <summary>
        /// A configuration for the <see cref="PlaidClient"/> instance to use the Plaid API server's "sandbox" subdomain.
        /// </summary>
        Sandbox
    }
}