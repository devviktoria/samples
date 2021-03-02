using MongoDB.Bson.Serialization.Attributes;

namespace jcwebapi.Models {
    public class Response {

        public const string SleepyEmotion = "sleepy";
        public const string NoneEmotion = "none";
        public const string HappyEmotion = "happy";
        public const string LolEmotion = "lol";
        public const string LshicEmotion = "lshic";

        public static readonly string[] Emotions = { SleepyEmotion, NoneEmotion, HappyEmotion, LolEmotion, LshicEmotion };

        [BsonElement (elementName: "emotion")]
        public string Emotion { get; set; }

        [BsonElement (elementName: "counter")]
        public int Counter { get; set; }
    }
}