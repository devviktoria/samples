using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace jcwebapi.Models {
    public class Joke {

        [BsonElement ("_id")]
        [JsonProperty ("Id")]
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement (elementName: "useremail")]
        public string UserEmail { get; set; }

        [BsonElement (elementName: "username")]
        public string UserName { get; set; }

        [BsonElement (elementName: "text")]
        public string Text { get; set; }

        [BsonElement (elementName: "source")]
        public string Source { get; set; }

        [BsonElement (elementName: "copyright")]
        public string Copyright { get; set; }

        [BsonElement (elementName: "creationdate")]
        public DateTime CreationDate { get; set; }

        [BsonElement (elementName: "releaseddate")]
        public DateTime? ReleasedDate { get; set; }

        [BsonElement (elementName: "tags")]
        public List<string> Tags { get; set; }

        [BsonElement (elementName: "emotioncounters")]
        public List<EmotionCounter> EmotionCounters { get; set; }

        [BsonElement (elementName: "responsesum")]
        public int ResponseSum { get; set; }

        [BsonElement (elementName: "responsestats")]
        public List<ResponseStatistic> ResponseStatistics { get; set; }
    }
}