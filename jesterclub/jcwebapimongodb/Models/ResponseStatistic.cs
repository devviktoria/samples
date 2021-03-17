using MongoDB.Bson.Serialization.Attributes;

namespace jcwebapi.Models {
    public class ResponseStatistic {
        [BsonElement (elementName: "day")]
        public int Day { get; set; }

        [BsonElement (elementName: "counter")]
        public int Counter { get; set; }
    }
}