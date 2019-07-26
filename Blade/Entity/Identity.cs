using System.Text.Json.Serialization;

namespace Blade.Entity
{
    /// <summary>
    /// Represents an account holder information.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>The names.</value>
        public string[] Names { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        public Address[] Addresses { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>The emails.</value>
        public Email[] Emails { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>The phone numbers.</value>
        public Phone[] PhoneNumbers { get; set; }

        /// <summary>
        /// Represents a <see cref="Identity"/> phone number.
        /// </summary>
        public class Phone
        {
            /// <summary>
            /// Gets or sets a value indicating whether this instance is the primary phone number.
            /// </summary>
            /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
            public bool Primary { get; set; }

            /// <summary>
            /// Gets or sets the number.
            /// </summary>
            /// <value>The number.</value>
            public string Data { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            /// <value>The type.</value>
            public string Type { get; set; }
        }

        /// <summary>
        /// Represents and <see cref="Identity"/> email.
        /// </summary>
        public class Email
        {
            /// <summary>
            /// Gets or sets the address.
            /// </summary>
            /// <value>The address.</value>
            public string Data { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is the primary email.
            /// </summary>
            /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
            public bool Primary { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            /// <value>The type.</value>
            public string Type { get; set; }
        }

        /// <summary>
        /// Represents an <see cref="Identity"/> address.
        /// </summary>
        public class Address
        {
            /// <summary>
            /// Gets or sets a value indicating whether this instance is the primary address.
            /// </summary>
            /// <value><c>true</c> if this instance is primary; otherwise, <c>false</c>.</value>
            public bool Primary { get; set; }

            /// <summary>
            /// Gets or sets the address data (city, state, etc).
            /// </summary>
            /// <value>The address data.</value>
            public AddressData Data { get; set; }

            /// <summary>
            /// Represents the fields of an address.
            /// </summary>
            public class AddressData
            {
                /// <summary>
                /// Gets or sets the full street address.
                /// </summary>
                /// <value>The full street address.</value>
                public string Street { get; set; }

                /// <summary>
                /// Gets or sets the full city name.
                /// </summary>
                /// <value>The city.</value>
                public string City { get; set; }

                /// <summary>
                /// Gets or sets the state or region.
                /// </summary>
                /// <value>The state or region.</value>
                public string Region { get; set; }

                /// <summary>
                /// Gets or sets the postal code.
                /// </summary>
                /// <value>The postal code.</value>
                public string PostalCode { get; set; }
            }
        }
    }
}