namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's '/item/webhook/update' endpoint. The '/item/webhook/update' endpoint allows you to update the webhook associated with an <see cref="Entity.Item"/>. This request triggers a WEBHOOK_UPDATE_ACKNOWLEDGED webhook. (https://plaid.com/docs/api/#item-webhooks).
    /// </summary>
    /// <seealso cref="Response" />
    public class UpdateWebhookResponse : Response
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Entity.Item Item { get; set; }
    }
}