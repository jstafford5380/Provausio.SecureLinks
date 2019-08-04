using System.Collections.Generic;
using Newtonsoft.Json;

namespace Provausio.SecureLink.Api
{
    public class ApiError
    {
        /// <summary>
        /// List of error messages.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        [JsonProperty("errors")]
        public List<string> Errors { get; set; } = new List<string>();
    }
}
