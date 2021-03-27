using jcinterfaces.Dto;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace jcmongodbservice.Models {
    public class ResponseStatistic : IResponseStatisticDto {
        public static void RegisterIResponseStatisticDtoSerialiazer () {
            var serializer = BsonSerializer.LookupSerializer<ResponseStatistic> ();

            BsonSerializer.RegisterSerializer<IResponseStatisticDto> (
                new ImpliedImplementationInterfaceSerializer<IResponseStatisticDto, ResponseStatistic> (serializer));
        }

        [BsonElement (elementName: "day")]
        public int Day { get; set; }

        [BsonElement (elementName: "counter")]
        public int Counter { get; set; }
    }
}