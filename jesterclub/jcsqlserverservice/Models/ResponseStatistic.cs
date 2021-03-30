using jcinterfaces.Dto;

namespace jcsqlserverservice.Models {
    public class ResponseStatistic : IResponseStatisticDto {

        public int Day { get; set; }

        public int Counter { get; set; }
    }
}