using System;

namespace Provausio.SecureLink.Application
{
    public class SecuredLink
    {
        /// <summary>
        /// The Link ID.
        /// </summary>
        /// <value>
        /// The link identifier.
        /// </value>
        public string LinkId { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> on which the link will expire.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset ExpiresAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuredLink"/> class.
        /// </summary>
        /// <param name="linkId">The link identifier.</param>
        /// <param name="expiresAt">The expires at.</param>
        public SecuredLink(string linkId, DateTimeOffset expiresAt)
        {
            LinkId = linkId;
            ExpiresAt = expiresAt;
        }
    }
}