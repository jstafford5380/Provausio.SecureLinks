using System.Collections.Generic;
using Ardalis.GuardClauses;
using MediatR;

namespace Provausio.SecureLink.Application.Commands
{
    public class CreateLinkCommand : IRequest<SecuredLink>
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypted { get; }

        /// <summary>
        /// Gets the secrets.
        /// </summary>
        /// <value>
        /// The secrets.
        /// </value>
        public IEnumerable<string> Secrets { get; }

        /// <summary>
        /// Gets the TTL seconds.
        /// </summary>
        /// <value>
        /// The TTL seconds.
        /// </value>
        public int TtlSeconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateLinkCommand"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
        /// <param name="secrets">The secrets.</param>
        /// <param name="ttlSeconds">The TTL seconds.</param>
        public CreateLinkCommand(string data, bool isEncrypted, ICollection<string> secrets, int ttlSeconds)
        {
            Guard.Against.Null(data, nameof(data));
            Guard.Against.NullOrEmpty(secrets, nameof(secrets));
            Guard.Against.OutOfRange(ttlSeconds, nameof(ttlSeconds), 1, int.MaxValue);

            Data = data;
            IsEncrypted = isEncrypted;
            Secrets = secrets;
            TtlSeconds = ttlSeconds;
        }
    }
}
