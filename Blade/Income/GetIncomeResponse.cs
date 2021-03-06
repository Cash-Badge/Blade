﻿namespace Blade.Income
{
    /// <summary>
    /// Represents a response from plaid's '/income/get' endpoint. The Income endpoint allows you to retrieve information pertaining to a Item’s income. In addition to the annual income, detailed information will be provided for each contributing income stream (or job). Details on each of these fields can be found below.
    /// </summary>
    /// <seealso cref="Response" />
    public class GetIncomeResponse : Response
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public Entity.Item Item { get; set; }

        /// <summary>
        /// Gets or sets the user income data.
        /// </summary>
        /// <value>The income data.</value>
        public Entity.Income Income { get; set; }
    }
}