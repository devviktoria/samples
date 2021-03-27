using jcinterfaces.Dto;
using Newtonsoft.Json.Linq;

namespace jcinterfaces.Service {
    public interface IJokeConverterService {
        public IJokeDto ConvertJObjectToIokeDto (JObject jObject);
    }
}