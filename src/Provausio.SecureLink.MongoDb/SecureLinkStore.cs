using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Provausio.SecureLink.Application;
using Provausio.SecureLink.Application.Services;

namespace Provausio.SecureLink.MongoDb
{
    public class SecureLinkStore : ISecuredLinkStore
    {
        private readonly IMongoCollection<SecureLinkData> _collection;

        public SecureLinkStore(IMongoCollection<SecureLinkData> collection)
        {
            _collection = collection;
        }

        public async Task SaveLinkAsync(
            IEnumerable<string> hashes, 
            string data, 
            bool isEncrypted, 
            DateTimeOffset expireAt,
            CancellationToken cancellationToken)
        {
            var docs = hashes.Select(h => new SecureLinkData
            {
                Hash = h,
                Data = data,
                ExpireAt = expireAt.DateTime,
                IsEncrypted = isEncrypted
            });

            await _collection.InsertManyAsync(docs, cancellationToken: cancellationToken);
        }

        public async Task<SecuredValue> GetDataAsync(string hash, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(d => d.Hash == hash, cancellationToken: cancellationToken);
            var docs = await cursor.ToListAsync(cancellationToken);
            var doc = docs.SingleOrDefault();

            return doc == null 
                ? SecuredValue.Empty
                : new SecuredValue(doc.Data, doc.IsEncrypted, doc.ExpireAt);
        }
    }
}
