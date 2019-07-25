namespace Blade.Management
{
    /// <summary>
    /// Represents a request for plaid's '/item/delete' endpoint. The '/item/delete' endpoint allows you to delete an <see cref="Entity.Item"/>. Once deleted, the access_token associated with the Item is no longer valid and cannot be used to access any data that was associated with the <see cref="Entity.Item"/>.
    /// </summary>
    /// <seealso cref="Request" />
    public class DeleteItemRequest : Request { }
}