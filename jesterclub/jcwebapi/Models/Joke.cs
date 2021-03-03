using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace jcwebapi.Models {
    public class Joke {
        [BsonElement ("_id")]
        [JsonProperty ("_id")]
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement (elementName: "userid")]
        public string UserId { get; set; }

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

        [BsonElement (elementName: "tags")]
        public List<string> Tags { get; set; }

        [BsonElement (elementName: "responses")]
        public List<Response> Responses { get; set; }

        [BsonElement (elementName: "responsesum")]
        public int ResponseSum { get; set; }

        [BsonElement (elementName: "responsestats")]
        public List<ResponseStatistic> ResponseStatistics { get; set; }
    }
}