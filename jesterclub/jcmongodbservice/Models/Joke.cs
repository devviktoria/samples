using System;
using System.Collections.Generic;
using jcinterfaces.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace jcmongodbservice.Models {
    public class Joke : IJokeDto {

        [BsonElement ("_id")]
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
        //[BsonSerializer (typeof (ImpliedImplementationInterfaceSerializer<List<IEmotionCounterDto>, List<EmotionCounter>>))]
        public List<IEmotionCounterDto> EmotionCounters { get; set; }

        [BsonElement (elementName: "responsesum")]
        public int ResponseSum { get; set; }

        [BsonElement (elementName: "responsestats")]
        //[BsonSerializer (typeof (ImpliedImplementationInterfaceSerializer<IResponseStatisticDto, ResponseStatistic>))]
        public List<IResponseStatisticDto> ResponseStatistics { get; set; }
    }
}