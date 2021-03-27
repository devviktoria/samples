using jcinterfaces.Dto;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace jcmongodbservice.Models {
    public class EmotionCounter : IEmotionCounterDto {

        public const string SleepyEmotion = "sleepy";
        public const string NoneEmotion = "none";
        public const string HappyEmotion = "happy";
        public const string LolEmotion = "lol";
        public const string LshicEmotion = "lshic";

        public static readonly string[] Emotions = { SleepyEmotion, NoneEmotion, HappyEmotion, LolEmotion, LshicEmotion };

        public static void RegisterIEmotionCounterDtoSerialiazer () {
            var serializer = BsonSerializer.LookupSerializer<EmotionCounter> ();

            BsonSerializer.RegisterSerializer<IEmotionCounterDto> (
                new ImpliedImplementationInterfaceSerializer<IEmotionCounterDto, EmotionCounter> (serializer));
        }

        [BsonElement (elementName: "emotion")]
        public string Emotion { get; set; }

        [BsonElement (elementName: "counter")]
        public int Counter { get; set; }
    }
}