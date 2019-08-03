using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Provausio.SecureLink.MongoDb
{
    [BsonIgnoreExtraElements]
    public class SecureLinkData
    {
        public string Hash { get; set; }

        public string Data { get; set; }

        public bool IsEncrypted { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ExpireAt { get; set; }
    }
}
