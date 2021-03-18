using System;
using System.Collections.Generic;
using System.Linq;

namespace jcwebapi.Models.Dtos {
    public class JokeDto {
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }

        public string Copyright { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ReleasedDate { get; set; }

        public List<string> Tags { get; set; }

        public List<EmotionCounter> EmotionCounters { get; set; }

        public int ResponseSum { get; set; }

        public List<ResponseStatistic> ResponseStatistics { get; set; }

        public static explicit operator JokeDto (Joke joke) {
            JokeDto jokeDto = new JokeDto {
                Id = joke.JokeId.ToString (),
                UserEmail = joke.User.UserEmail,
                UserName = joke.User.UserName,
                Text = joke.JokeText,
                Source = joke.Source,
                Copyright = joke.Copyright,
                CreationDate = joke.CreationDate,
                ReleasedDate = joke.ReleasedDate,
                Tags = joke.Tags.Select (t => t.Name).ToList<string> (),
                EmotionCounters = joke.EmotionCounters.ToList (),
                ResponseSum = joke.ResponseSum,
                ResponseStatistics = joke.ResponseStatistics.ToList ()
            };

            return jokeDto;
        }
    }
}