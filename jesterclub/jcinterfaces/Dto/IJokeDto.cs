using System;
using System.Collections.Generic;

namespace jcinterfaces.Dto {
    public interface IJokeDto {
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

    }
}