using System;
using System.Collections.Generic;
using System.Text;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's '/item/public_token/create' endpoint.
    /// </summary>
    public class CreatePublicTokenResponse : Response
    {
        /// <summary>
        /// The public token which can be temporarily used to refer to the target <see cref="Entity.Item"/> on Plaid's servers.
        /// </summary>
        public string PublicToken { get; set; }
    }
}
