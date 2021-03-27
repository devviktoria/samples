using System;
using System.Collections.Generic;
using jcinterfaces.Dto;
using jcinterfaces.Service;
using jcmongodbservice.Models;
using Newtonsoft.Json.Linq;

namespace jcmongodbservice.Services {
    public class JokeConverterService : IJokeConverterService {
        public IJokeDto ConvertJObjectToIokeDto (JObject jObject) {
            Joke joke = new jcmongodbservice.Models.Joke ();
            joke.Id = jObject["Id"].Value<string> ();
            joke.UserEmail = jObject["UserEmail"].Value<string> ();
            joke.UserName = jObject["UserName"].Value<string> ();
            joke.Text = jObject["Text"].Value<string> ();
            joke.Source = jObject["Source"].Value<string> ();
            joke.Copyright = jObject["Copyright"].Value<string> ();
            joke.CreationDate = jObject["CreationDate"].Value<DateTime> ();
            joke.ReleasedDate = jObject["ReleasedDate"] == null ? null : jObject["ReleasedDate"].Value<DateTime?> ();
            joke.Tags = new List<string> ();
            foreach (string tag in jObject["Tags"].Values<string> ()) {
                joke.Tags.Add (tag);
            }

            joke.ResponseSum = 0;
            joke.ResponseStatistics = new List<IResponseStatisticDto> ();
            if (jObject["ResponseStatistics"] != null) {
                foreach (JObject jObjectRS in jObject["ResponseStatistics"].Values<JObject> ()) {
                    ResponseStatistic responseStatistic = new ResponseStatistic ();
                    responseStatistic.Day = jObjectRS["Day"].Value<int> ();
                    responseStatistic.Counter = jObjectRS["Counter"].Value<int> ();
                    joke.ResponseStatistics.Add (responseStatistic);
                }
            }

            joke.EmotionCounters = new List<IEmotionCounterDto> ();
            if (jObject["EmotionCounters"] != null) {
                foreach (JObject jObjectEC in jObject["EmotionCounters"].Values<JObject> ()) {
                    EmotionCounter emotionCounter = new EmotionCounter ();
                    emotionCounter.Emotion = jObjectEC["Emotion"].Value<string> ();
                    emotionCounter.Counter = jObjectEC["Counter"].Value<int> ();
                    joke.EmotionCounters.Add (emotionCounter);
                }
            }

            return joke;

        }

    }
}