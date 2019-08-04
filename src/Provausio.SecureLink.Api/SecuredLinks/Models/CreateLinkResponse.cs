using System;
using Newtonsoft.Json;
using RiskFirst.Hateoas.Models;

namespace Provausio.SecureLink.Api.SecuredLinks.Models
{
    public class CreateLinkResponse : LinkContainer
    {
        /// <summary>
        /// The link identifier.
        /// </summary>
        /// <value>
        /// The link identifier.
        /// </value>
        [JsonProperty("linkId")]
        public string LinkId { get; set; }

        /// <summary>
        /// DateTime on which the link will expire.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        [JsonProperty("expiresAt")]
        public DateTimeOffset ExpiresAt { get; set; }
    }
}