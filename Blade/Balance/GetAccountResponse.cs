namespace Blade.Balance
{
    /// <summary>
    /// Represents a response from plaid's '/accounts/get' endpoint.
    /// </summary>
    /// <seealso cref="Response" />
    public class GetAccountResponse : Response
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Entity.Item Item { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>The accounts.</value>
        public Entity.Account[] Accounts { get; set; }
    }
}