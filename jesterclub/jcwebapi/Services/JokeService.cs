using System.Collections.Generic;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Repository;
using MongoDB.Driver;

namespace jcwebapi.Services {
    public class JokeService {
        private readonly IMongoCollection<Joke> _jokeCollection;

        public JokeService (IMongoClient mongoClient) {
            _jokeCollection = mongoClient.GetDatabase (MongoDbDatabase.DbName).GetCollection<Joke> ("joke");
        }
        public async Task<Joke> Get (string id) =>
            await _jokeCollection.Find<Joke> (joke => joke.Id == id).FirstOrDefaultAsync ();

        public async Task<Joke> Create (Joke joke) {
            if (joke.Id != null) {
                joke.Id = null;
            }

            joke.Responses = new List<Response> ();
            foreach (string emotion in Response.Emotions) {
                joke.Responses.Add (new Response () { Emotion = emotion, Counter = 0 });
            }

            joke.ResponseSum = 0;

            joke.ResponseStatistics = new List<ResponseStatistic> ();
            for (int day = 1; day <= 7; day++) {
                joke.ResponseStatistics.Add (new ResponseStatistic () { Day = day, Counter = 0 });
            }

            await _jokeCollection.InsertOneAsync (joke);
            return joke;
        }

        public async Task Update (string id, Joke jokeIn) =>
            await _jokeCollection.ReplaceOneAsync (joke => joke.Id == id, jokeIn);

        public async Task Remove (Joke jokeIn) =>
            await _jokeCollection.DeleteOneAsync (joke => joke.Id == jokeIn.Id);

        public async Task Remove (string id) =>
            await _jokeCollection.DeleteOneAsync (joke => joke.Id == id);
    }
}