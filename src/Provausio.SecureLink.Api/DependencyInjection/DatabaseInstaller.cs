using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Provausio.SecureLink.Application.Services;
using Provausio.SecureLink.MongoDb;

namespace Provausio.SecureLink.Api.DependencyInjection
{
    public static class DatabaseInstaller
    {
        public static void AddDatabases(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(p =>
            {
                var connectionString = config["MongoDb:ConnectionString"];
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("secureLink");
                return database;
            });

            services.AddTransient(p =>
            {
                var database = p.GetRequiredService<IMongoDatabase>();
                var collection = database.GetCollection<SecureLinkData>("secureLinks");

                var expireAtIndex = new CreateIndexModel<SecureLinkData>(
                    Builders<SecureLinkData>.IndexKeys.Ascending(r => r.ExpireAt),
                    new CreateIndexOptions {ExpireAfter = TimeSpan.FromSeconds(0)});

                var uniqueHashIndex = new CreateIndexModel<SecureLinkData>(
                    Builders<SecureLinkData>.IndexKeys.Ascending(i => i.Hash),
                    new CreateIndexOptions {Unique = true});

                collection.Indexes.CreateMany(new[] { expireAtIndex, uniqueHashIndex });

                return collection;
            });

            services.AddTransient<ISecuredLinkStore, SecureLinkStore>();
        }
    }
}
