using MediatR;

namespace Provausio.SecureLink.Application.Queries
{
    public class DecodeLinkQuery : IRequest<SecuredValue>
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        public string Hash { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeLinkQuery"/> class.
        /// </summary>
        /// <param name="hash">The hash.</param>
        public DecodeLinkQuery(string hash)
        {
            Hash = hash;
        }
    }
}
