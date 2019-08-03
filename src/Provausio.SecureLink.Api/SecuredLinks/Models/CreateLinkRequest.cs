using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Provausio.SecureLink.Api.SecuredLinks.Models
{
    public class CreateLinkRequest
    {
        /// <summary>
        /// A list of client secrets that will be used to secure the data.
        /// </summary>
        /// <value>
        /// The secrets.
        /// </value>
        [Required, JsonProperty("secrets")]
        public List<string> Secrets { get; set; }


        /// <summary>
        /// The data to be secured.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [Required, JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Whether this instance is encrypted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        [Required, JsonProperty("isEncrypted")]
        public bool IsEncrypted { get; set; }

        /// <summary>
        /// The time to live, in seconds.
        /// </summary>
        /// <value>
        /// The TTL seconds.
        /// </value>
        [Required, JsonProperty("ttlSeconds")]
        public int TtlSeconds { get; set; }
    }
}