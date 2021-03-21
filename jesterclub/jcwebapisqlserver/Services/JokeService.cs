using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Models.Dtos;
using jcwebapi.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace jcwebapi.Services {
    public class JokeService {
        private const int LatestJokeCount = 10;
        private const int MostPopularJokeCount = 10;
        private SqlServerDbContext _sqlServerDbContext;

        public JokeService (SqlServerDbContext sqlServerDbContext) {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<JokeDto> Get (string id) {
            int sqlId;
            if (!Int32.TryParse (id, out sqlId)) {
                throw new InvalidJokeIdException ($"{id} is not a valid joke id.");
            }
            Joke joke = await _sqlServerDbContext.Jokes.SingleAsync (j => j.JokeId == sqlId);
            await _sqlServerDbContext.Entry (joke).Reference (j => j.User).LoadAsync ();
            await _sqlServerDbContext.Entry (joke).Collection (j => j.Tags).LoadAsync ();
            await _sqlServerDbContext.Entry (joke).Collection (j => j.EmotionCounters).LoadAsync ();
            await _sqlServerDbContext.Entry (joke).Collection (j => j.ResponseStatistics).LoadAsync ();
            return (JokeDto) (joke);
        }

        public async Task<IReadOnlyList<JokeDto>> GetLatestJokes (CancellationToken cancellationToken) {
            return await _sqlServerDbContext.Jokes
                .Where (j => j.ReleasedDate != null)
                .OrderByDescending (j => j.ReleasedDate)
                .Take (LatestJokeCount)
                .Include (j => j.User)
                .Include (j => j.Tags)
                .Include (j => j.EmotionCounters)
                .Include (j => j.ResponseStatistics)
                .Select (j => (JokeDto) j)
                .ToListAsync ();
        }

        public async Task<IReadOnlyList<JokeDto>> GetMostPopularJokes (CancellationToken cancellationToken) {
            return await _sqlServerDbContext.Jokes
                .Where (j => j.ReleasedDate != null)
                .OrderByDescending (j => j.ResponseSum)
                .Take (MostPopularJokeCount)
                .Include (j => j.User)
                .Include (j => j.Tags)
                .Include (j => j.EmotionCounters)
                .Include (j => j.ResponseStatistics)
                .Select (j => (JokeDto) j)
                .ToListAsync ();
        }

        public async Task<IReadOnlyList<JokeDto>> GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken) {
            Expression<Func<Joke, bool>> modeJokesPredicate;

            if (mode.Equals ("draft", StringComparison.OrdinalIgnoreCase)) {
                modeJokesPredicate = (j => j.User.UserEmail == userEmail && j.ReleasedDate == null);
            } else {
                modeJokesPredicate = (j => j.User.UserEmail == userEmail && j.ReleasedDate != null);
            }

            return await _sqlServerDbContext.Jokes
                .Where (modeJokesPredicate)
                .OrderBy (j => j.CreationDate)
                .Skip (pageIndex * jokesPerPage)
                .Take (jokesPerPage)
                .Include (j => j.User)
                .Include (j => j.Tags)
                .Include (j => j.EmotionCounters)
                .Include (j => j.ResponseStatistics)
                .Select (j => (JokeDto) j)
                .ToListAsync ();
        }

        public async Task<JokeDto> Create (JokeDto jokeDto) {
            Joke joke = new Joke ();
            joke.JokeId = null;
            setupJokeData (joke, jokeDto);

            User user = await _sqlServerDbContext.Users.SingleAsync<User> (u => u.UserEmail == jokeDto.UserEmail);

            if (user == null) {
                throw new UserNotFoundException ($"The user with email: {jokeDto.UserEmail} does not exist in the database!");
            }

            joke.User = user;

            setupJokeTags (joke, jokeDto.Tags);

            joke.EmotionCounters = new List<EmotionCounter> ();
            foreach (string emotion in EmotionCounter.Emotions) {
                joke.EmotionCounters.Add (new EmotionCounter () { Emotion = emotion, Counter = 0 });
            }

            joke.ResponseSum = 0;

            joke.ResponseStatistics = new List<ResponseStatistic> ();
            for (int day = 1; day <= 7; day++) {
                joke.ResponseStatistics.Add (new ResponseStatistic () { Day = day, Counter = 0 });
            }
            _sqlServerDbContext.Jokes.Add (joke);
            await _sqlServerDbContext.SaveChangesAsync ();
            return (JokeDto) joke;
        }

        public async Task Update (string id, JokeDto jokeIn) {
            int sqlId;
            if (!Int32.TryParse (id, out sqlId)) {
                throw new InvalidJokeIdException ($"{id} is not a valid joke id.");
            }

            Joke joke = await _sqlServerDbContext.Jokes
                .Where (j => j.JokeId == sqlId)
                .Include (j => j.User)
                .Include (j => j.Tags)
                .Include (j => j.EmotionCounters)
                .Include (j => j.ResponseStatistics)
                .FirstAsync ();;

            if (joke == null) {
                throw new JokeNotFoundException ($"The joke with id:{id} not exists in the database.");
            }

            setupJokeData (joke, jokeIn);
            setupJokeTags (joke, jokeIn.Tags);
            await _sqlServerDbContext.SaveChangesAsync ();
        }

        public async Task<JokeDto> IncrementEmotionCounter (string id, string emotion) {
            try {
                SqlParameter jokeIdParam = new SqlParameter ("@jokeId", id);
                SqlParameter emotionParam = new SqlParameter ("@emotion", emotion);
                await _sqlServerDbContext.Database.ExecuteSqlRawAsync ("EXECUTE dbo.UpdateEmotionCounters @jokeId, @emotion", jokeIdParam, emotionParam);
                return await Get (id);
            } catch (SqlException sqlE) {
                throw new JokeNotFoundException (sqlE.ToString ());
            }
        }

        public async Task Remove (string id) {
            int sqlId;
            if (!Int32.TryParse (id, out sqlId)) {
                throw new InvalidJokeIdException ($"{id} is not a valid joke id.");
            }

            Joke joke = await _sqlServerDbContext.Jokes.SingleAsync (j => j.JokeId == sqlId);

            if (joke == null) {
                throw new JokeNotFoundException ($"The joke with id:{id} not exists in the database.");
            }

            _sqlServerDbContext.Jokes.Remove (joke);
            await _sqlServerDbContext.SaveChangesAsync ();
        }

        private void setupJokeData (Joke joke, JokeDto jokeDto) {
            joke.JokeText = jokeDto.Text;
            joke.Source = jokeDto.Source;
            joke.Copyright = jokeDto.Copyright;
            joke.CreationDate = jokeDto.CreationDate;
            joke.ReleasedDate = jokeDto.ReleasedDate;
        }

        private async void setupJokeTags (Joke joke, IEnumerable<string> tags) {
            List<Tag> existingTags = await _sqlServerDbContext.Tags.Where (t => tags.Contains (t.Name)).ToListAsync ();

            joke.Tags = existingTags;
            foreach (string tag in tags) {
                if (!existingTags.Any (t => t.Name.Equals (tag, StringComparison.InvariantCultureIgnoreCase))) {
                    joke.Tags.Add (new Tag { Name = tag.ToLowerInvariant () });
                }
            }
        }

    }
}