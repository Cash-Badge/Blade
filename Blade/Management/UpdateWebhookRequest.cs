namespace Blade.Management
{
    /// <summary>
    /// Represents a request for plaid's '/item/webhook/update' endpoint. The '/item/webhook/update' endpoint allows you to update the webhook associated with an <see cref="Entity.Item"/>. This request triggers a WEBHOOK_UPDATE_ACKNOWLEDGED webhook. (https://plaid.com/docs/api/#item-webhooks)
    /// </summary>
    /// <seealso cref="Request" />
    public class UpdateWebhookRequest : Request
    {
        /// <summary>
        /// Gets or sets the webhook.
        /// </summary>
        /// <value>The webhook.</value>
        public string Webhook { get; set; }
    }
}