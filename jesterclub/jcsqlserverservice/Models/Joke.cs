using System;
using System.Collections.Generic;

namespace jcsqlserverservice.Models {
    public class Joke {

        public int? JokeId { get; set; }

        public string JokeText { get; set; }

        public string Source { get; set; }

        public string Copyright { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ReleasedDate { get; set; }

        public int ResponseSum { get; set; }

        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<EmotionCounter> EmotionCounters { get; set; }

        public ICollection<ResponseStatistic> ResponseStatistics { get; set; }
    }
}