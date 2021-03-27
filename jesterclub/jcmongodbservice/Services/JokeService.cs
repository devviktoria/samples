using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jcinterfaces.Dto;
using jcinterfaces.Service;
using jcmongodbservice.Models;
using jcmongodbservice.Repository;
using MongoDB.Driver;

namespace jcmongodbservice.Services {
    public class JokeService : IJokeService {
        private const int LatestJokeCount = 10;
        private const int MostPopularJokeCount = 10;

        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Joke> _jokeCollection;

        public JokeService (IMongoClient mongoClient) {
            _mongoClient = mongoClient;
            _jokeCollection = mongoClient.GetDatabase (MongoDbDatabase.DbName).GetCollection<Joke> ("joke");
        }

        public async Task<IJokeDto> Get (string id) =>
            await _jokeCollection.Find<Joke> (joke => joke.Id == id).FirstOrDefaultAsync ();

        public async Task<IReadOnlyList<IJokeDto>> GetLatestJokes (CancellationToken cancellationToken) {
            var releasedJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            return await _jokeCollection
                .Find (releasedJokesFilter)
                .SortByDescending (j => j.ReleasedDate)
                .Limit (LatestJokeCount)
                .ToListAsync ();
        }

        public async Task<IReadOnlyList<IJokeDto>> GetMostPopularJokes (CancellationToken cancellationToken) {
            var releasedJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            return await _jokeCollection
                .Find (releasedJokesFilter)
                .SortByDescending (j => j.ResponseSum)
                .Limit (MostPopularJokeCount)
                .ToListAsync ();
        }

        public async Task<IReadOnlyList<IJokeDto>> GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken) {
            FilterDefinition<Joke> modeJokesFilter;
            if (mode.Equals ("draft", StringComparison.OrdinalIgnoreCase)) {
                modeJokesFilter = Builders<Joke>.Filter.Eq (j => j.ReleasedDate, null);
            } else {
                modeJokesFilter = Builders<Joke>.Filter.Ne (j => j.ReleasedDate, null);
            }
            var emailFilter = Builders<Joke>.Filter.Eq (j => j.UserEmail, userEmail);
            var jokesFilter = Builders<Joke>.Filter.And (modeJokesFilter, emailFilter);

            return await _jokeCollection
                .Find (jokesFilter)
                .SortBy (j => j.CreationDate)
                .Limit (jokesPerPage)
                .Skip (pageIndex * jokesPerPage)
                .ToListAsync ();
        }

        public async Task<IJokeDto> Create (IJokeDto joke) {
            if (joke.Id != null) {
                joke.Id = null;
            }

            joke.EmotionCounters = new List<IEmotionCounterDto> ();
            foreach (string emotion in EmotionCounter.Emotions) {
                joke.EmotionCounters.Add (new EmotionCounter () { Emotion = emotion, Counter = 0 });
            }

            joke.ResponseSum = 0;

            joke.ResponseStatistics = new List<IResponseStatisticDto> ();
            for (int day = 1; day <= 7; day++) {
                joke.ResponseStatistics.Add (new ResponseStatistic () { Day = day, Counter = 0 });
            }

            await _jokeCollection.InsertOneAsync ((Joke) joke);
            return joke;
        }

        public async Task Update (string id, IJokeDto jokeIn) {
            IJokeDto joke = await Get (id);
            if (joke == null) {
                throw new JokeNotFoundException ($"The joke with the id: {id} does not exists.");
            }
            await _jokeCollection.ReplaceOneAsync (joke => joke.Id == id, (Joke) jokeIn);
        }

        public async Task<IJokeDto> IncrementEmotionCounter (string id, string emotion) {
            if (!Array.Exists (EmotionCounter.Emotions, e => e == emotion)) {
                throw new ArgumentException ($"The give emotion:{emotion} is not a valid emotion.");
            }

            IJokeDto joke = await Get (id);
            if (joke == null) {
                throw new JokeNotFoundException ($"The joke with the id: {id} does not exists.");
            }

            using (var session = await _mongoClient.StartSessionAsync ()) {
                try {
                    session.StartTransaction ();
                    var idFilter = Builders<Joke>.Filter.Eq (j => j.Id, id);

                    var emotionCounterFilter = idFilter & Builders<Joke>.Filter.Eq ("emotioncounters.emotion", emotion);
                    var emotionCounterUpdateBuilder = Builders<Joke>.Update.Inc ("emotioncounters.$.counter", 1);

                    var emotionCounterUpdateResult = await _jokeCollection.UpdateOneAsync (emotionCounterFilter, emotionCounterUpdateBuilder);
                    if (emotionCounterUpdateResult.ModifiedCount > 0) {
                        int utcDayOfWeek = (int) DateTime.UtcNow.DayOfWeek;
                        if (utcDayOfWeek == 0) {
                            utcDayOfWeek = 7;
                        }

                        var responseStatsFilter = idFilter & Builders<Joke>.Filter.Eq ("responsestats.day", utcDayOfWeek);
                        var responseStatsUpdateBuilder = Builders<Joke>.Update.Inc ("responsestats.$.counter", 1);
                        var responseStatsUpdateResult = await _jokeCollection.UpdateOneAsync (responseStatsFilter, responseStatsUpdateBuilder);

                        if (responseStatsUpdateResult.ModifiedCount > 0) {
                            Joke updatedJoke = (Joke) await Get (id);
                            int sumResponses = updatedJoke.ResponseStatistics.Sum (s => s.Counter);

                            var responseSumUpdateBuilder = Builders<Joke>.Update.Set (j => j.ResponseSum, sumResponses);
                            await _jokeCollection.UpdateOneAsync (idFilter, responseSumUpdateBuilder);
                        } else {
                            throw new JokeNotUpdatedException ("Failed to update the joke's response statictics!");
                        }
                    } else {
                        throw new JokeNotUpdatedException ("Failed to update the joke's emotion counters!");
                    }

                    await session.CommitTransactionAsync ();
                } catch {
                    await session.AbortTransactionAsync ();
                    throw;
                }

                return await Get (id);

            }

        }

        public async Task Remove (string id) {
            IJokeDto joke = await Get (id);
            if (joke == null) {
                throw new JokeNotFoundException ($"The joke with the id: {id} does not exists.");
            }
            await _jokeCollection.DeleteOneAsync (joke => joke.Id == id);
        }
    }
}