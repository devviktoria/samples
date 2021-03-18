using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Repository;

namespace jcwebapi.Services {
    public class JokeService {
        private const int LatestJokeCount = 10;
        private const int MostPopularJokeCount = 10;

        public JokeService () {
            // _mongoClient = mongoClient;
            // _jokeCollection = mongoClient.GetDatabase (MongoDbDatabase.DbName).GetCollection<Joke> ("joke");
        }

        public async Task<Joke> Get (string id) => null;
        // await _jokeCollection.Find<Joke> (joke => joke.Id == id).FirstOrDefaultAsync ();

        public async Task<IReadOnlyList<Joke>> GetLatestJokes (CancellationToken cancellationToken) {
            // var releasedJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            // return await _jokeCollection
            //     .Find (releasedJokesFilter)
            //     .SortByDescending (j => j.ReleasedDate)
            //     .Limit (LatestJokeCount)
            //     .ToListAsync ();
            return null;
        }

        public async Task<IReadOnlyList<Joke>> GetMostPopularJokes (CancellationToken cancellationToken) {
            //     var releasedJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            //     return await _jokeCollection
            //         .Find (releasedJokesFilter)
            //         .SortByDescending (j => j.ResponseSum)
            //         .Limit (MostPopularJokeCount)
            //         .ToListAsync ();
            return null;
        }

        public async Task<IReadOnlyList<Joke>> GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken) {
            // FilterDefinition<Joke> modeJokesFilter;
            // if (mode.Equals ("draft", StringComparison.OrdinalIgnoreCase)) {
            //     modeJokesFilter = Builders<Joke>.Filter.Eq (j => j.ReleasedDate, null);
            // } else {
            //     modeJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            // }
            // var emailFilter = Builders<Joke>.Filter.Eq (j => j.UserEmail, userEmail);
            // var jokesFilter = Builders<Joke>.Filter.And (modeJokesFilter, emailFilter);

            // return await _jokeCollection
            //     .Find (jokesFilter)
            //     .SortBy (j => j.CreationDate)
            //     .Limit (jokesPerPage)
            //     .Skip (pageIndex * jokesPerPage)
            //     .ToListAsync ();
            return null;
        }

        public async Task<Joke> Create (Joke joke) {
            // if (joke.Id != null) {
            //     joke.Id = null;
            // }

            // joke.EmotionCounters = new List<EmotionCounter> ();
            // foreach (string emotion in EmotionCounter.Emotions) {
            //     joke.EmotionCounters.Add (new EmotionCounter () { Emotion = emotion, Counter = 0 });
            // }

            // joke.ResponseSum = 0;

            // joke.ResponseStatistics = new List<ResponseStatistic> ();
            // for (int day = 1; day <= 7; day++) {
            //     joke.ResponseStatistics.Add (new ResponseStatistic () { Day = day, Counter = 0 });
            // }

            // await _jokeCollection.InsertOneAsync (joke);
            // return joke;
            return null;
        }

        public async Task Update (string id, Joke jokeIn) { }
        //await _jokeCollection.ReplaceOneAsync (joke => joke.Id == id, jokeIn);

        public async Task<Joke> IncrementEmotionCounter (string id, string emotion) {
            return await Get (id);

        }

        public async Task Remove (Joke jokeIn) { }
        //await _jokeCollection.DeleteOneAsync (joke => joke.Id == jokeIn.Id);

        public async Task Remove (int id) { }
        //await _jokeCollection.DeleteOneAsync (joke => joke.Id == id);
    }
}