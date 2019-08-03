using System;
using System.Threading;
using System.Threading.Tasks;

namespace Provausio.SecureLink.Application.Services
{
    public interface ISecuredLinkService
    {
        /// <summary>
        /// Creates a link to the provided data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isEncrypted">Whether or not the provided data is encrypted.</param>
        /// <param name="secrets">A collection of client secrets for which links will be generated.</param>
        /// <param name="expireAt">DateTime on which the link should expire.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SecuredLink> CreateLinkAsync(string data, bool isEncrypted, string[] secrets, DateTimeOffset expireAt, CancellationToken cancellationToken);
    }
}