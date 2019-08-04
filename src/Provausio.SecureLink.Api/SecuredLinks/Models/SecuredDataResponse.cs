using System;
using Newtonsoft.Json;
using RiskFirst.Hateoas.Models;

namespace Provausio.SecureLink.Api.SecuredLinks.Models
{
    public class SecuredDataResponse : LinkContainer
    {
        /// <summary>
        /// The secured data.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isEncrypted")]
        public bool IsEncrypted { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        [JsonProperty("expiresAt")]
        public DateTimeOffset ExpiresAt { get; set; }
    }
}