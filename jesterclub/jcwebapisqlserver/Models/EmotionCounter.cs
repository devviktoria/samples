namespace jcwebapi.Models {
    public class EmotionCounter {

        public const string SleepyEmotion = "sleepy";
        public const string NoneEmotion = "none";
        public const string HappyEmotion = "happy";
        public const string LolEmotion = "lol";
        public const string LshicEmotion = "lshic";

        public static readonly string[] Emotions = { SleepyEmotion, NoneEmotion, HappyEmotion, LolEmotion, LshicEmotion };

        public string Emotion { get; set; }

        public int Counter { get; set; }
    }
}