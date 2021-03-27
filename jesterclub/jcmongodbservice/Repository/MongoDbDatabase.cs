using jcinterfaces.Service;
using jcmongodbservice.Models;
using jcmongodbservice.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace jcmongodbservice.Repository {
    public static class MongoDbDatabase {
        public const string DbName = "jesterclubdb";
        public static void RegisterMongoDbServices (IServiceCollection servicesBuilder) {
            EmotionCounter.RegisterIEmotionCounterDtoSerialiazer ();
            ResponseStatistic.RegisterIResponseStatisticDtoSerialiazer ();

            servicesBuilder.AddSingleton<IMongoClient, MongoClient> (s => {
                string mongoDbUri = System.Environment.GetEnvironmentVariable ("JCMDB");
                return new MongoClient (mongoDbUri);
            });

            servicesBuilder.AddSingleton<IJokeService, JokeService> ();
            servicesBuilder.AddSingleton<IJokeConverterService, JokeConverterService> ();
        }
    }
}