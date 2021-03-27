using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcinterfaces.Dto;

namespace jcinterfaces.Service {
    public interface IJokeService {

        public Task<IJokeDto> Get (string id);

        public Task<IReadOnlyList<IJokeDto>> GetLatestJokes (CancellationToken cancellationToken);

        public Task<IReadOnlyList<IJokeDto>> GetMostPopularJokes (CancellationToken cancellationToken);

        public Task<IReadOnlyList<IJokeDto>> GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken);

        public Task<IJokeDto> Create (IJokeDto jokeDto);

        public Task Update (string id, IJokeDto jokeIn);

        public Task<IJokeDto> IncrementEmotionCounter (string id, string emotion);

        public Task Remove (string id);

    }
}