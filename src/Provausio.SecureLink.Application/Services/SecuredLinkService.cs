using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Provausio.SecureLink.Application.Services
{
    public class SecuredLinkService : ISecuredLinkService
    {
        private readonly ISecuredLinkStore _linkStore;
        private readonly ILogger _logger;

        public SecuredLinkService(ISecuredLinkStore linkStore, ILogger<SecuredLinkService> logger)
        {
            _linkStore = linkStore;
            _logger = logger;
        }

        public async Task<SecuredLink> CreateLinkAsync(
            string data, 
            bool isEncrypted,
            string[] secrets, 
            DateTimeOffset expireAt, CancellationToken cancellationToken)
        {
            var newId = LinkIdGenerator.Next();
            
            var attempts = 0;
            var success = false;
            while (!success && attempts < 5)
            {
                try
                {
                    var hashes = GetHashes(newId, secrets);
                    await _linkStore.SaveLinkAsync(hashes, data, isEncrypted, expireAt, cancellationToken);
                    success = true;
                }
                catch (DuplicateHashException ex)
                {
                    _logger.LogError(ex.Message);
                    attempts++;
                }
            }

            if(!success)
                throw new LinkGenerationFailure(attempts);

            return new SecuredLink(newId, expireAt);
        }
        

        public IEnumerable<string> GetHashes(string linkId, IEnumerable<string> secrets)
        {
            var links = secrets.Select(s => CalculateHash(linkId, s));
            return links;
        }

        private static string CalculateHash( string linkId, string secret)
        {
            using var sha = new SHA1Managed();
            var input = $"{linkId}|{secret}";
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            var encoded = string.Concat(hash.Select(b => b.ToString("x2")));
            return encoded;
        }
    }
}