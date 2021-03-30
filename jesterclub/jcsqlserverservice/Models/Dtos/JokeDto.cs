using System;
using System.Collections.Generic;
using System.Linq;
using jcinterfaces.Dto;

namespace jcsqlserverservice.Models.Dtos {
    public class JokeDto : IJokeDto {
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }

        public string Copyright { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ReleasedDate { get; set; }

        public List<string> Tags { get; set; }

        public List<IEmotionCounterDto> EmotionCounters { get; set; }

        public int ResponseSum { get; set; }

        public List<IResponseStatisticDto> ResponseStatistics { get; set; }

        public static explicit operator JokeDto (Joke joke) {
            if (joke == null) {
                return null;
            }

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
                EmotionCounters = new List<IEmotionCounterDto> (),
                ResponseSum = joke.ResponseSum,
                ResponseStatistics = new List<IResponseStatisticDto> ()
            };

            jokeDto.EmotionCounters.AddRange (joke.EmotionCounters);
            jokeDto.ResponseStatistics.AddRange (joke.ResponseStatistics);
            return jokeDto;
        }
    }
}