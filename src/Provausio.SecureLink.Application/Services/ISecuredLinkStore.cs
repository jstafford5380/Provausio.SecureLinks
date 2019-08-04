using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Provausio.SecureLink.Application.Services
{
    public interface ISecuredLinkStore
    {
        /// <summary>
        /// Creates a record for each hash with the provided data.
        /// </summary>
        /// <param name="hashes">The hashes.</param>
        /// <param name="data">The data.</param>
        /// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
        /// <param name="expireAt">The expire at.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="DuplicateHashException"></exception>
        Task SaveLinkAsync(
            IEnumerable<string> hashes, 
            string data, 
            bool isEncrypted, 
            DateTimeOffset expireAt, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the data.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SecuredValue> GetDataAsync(string hash, CancellationToken cancellationToken);
    }
}
