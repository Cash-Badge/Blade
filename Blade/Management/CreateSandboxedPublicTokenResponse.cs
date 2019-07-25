using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Blade.Management
{
    /// <summary>
    /// Represents a response from plaid's '/sandbox/public_token/create' endpoint.
    /// </summary>
    public class CreateSandboxedPublicTokenResponse : Response
    {
        /// <summary>
        /// The public token associated with the newly-created <see cref="Entity.Item"/>.
        /// </summary>
        public string PublicToken { get; set; }
    }
}
