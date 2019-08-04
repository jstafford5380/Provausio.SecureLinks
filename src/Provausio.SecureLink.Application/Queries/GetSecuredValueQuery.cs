using MediatR;

namespace Provausio.SecureLink.Application.Queries
{
    public class GetSecuredValueQuery : IRequest<SecuredValue>
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        public string Hash { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSecuredValueQuery"/> class.
        /// </summary>
        /// <param name="hash">The hash.</param>
        public GetSecuredValueQuery(string hash)
        {
            Hash = hash;
        }
    }
}
