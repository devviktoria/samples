using jcwebapi.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace jcwebapi.Repository {
    public static class MongoDbDatabase {
        public const string DbName = "jesterclubdb";
        public static void RegisterMongoDbServices (IServiceCollection servicesBuilder) {
            servicesBuilder.AddSingleton<IMongoClient, MongoClient> (s => {
                string mongoDbUri = System.Environment.GetEnvironmentVariable ("JCMDB");
                return new MongoClient (mongoDbUri);
            });

            servicesBuilder.AddSingleton<JokeService> ();
        }
    }
}