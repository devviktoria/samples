using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcinterfaces.Dto;
using jcinterfaces.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace jcwebapi.Controllers {

    [EnableCors ("_JesterClubSpecificOrigins")]
    [Route ("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase {
        private readonly IJokeService _jokeService;
        private readonly IJokeConverterService _jokeConverterService;

        public JokeController (IJokeService jokeService, IJokeConverterService jokeConverterService) {
            _jokeService = jokeService;
            _jokeConverterService = jokeConverterService;
        }

        [HttpGet ("{id}", Name = "GetJoke")]
        public async Task<ActionResult<IJokeDto>> Get (string id) {
            var joke = await _jokeService.Get (id);

            if (joke == null) {
                return NotFound ();
            }

            return Ok (joke);
        }

        [HttpGet (nameof (GetLatestJokes), Name = nameof (GetLatestJokes))]
        public async Task<ActionResult<IReadOnlyList<IJokeDto>> > GetLatestJokes (CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetLatestJokes (cancellationToken));
        }

        [HttpGet (nameof (GetMostPopularJokes), Name = nameof (GetMostPopularJokes))]
        public async Task<ActionResult<IReadOnlyList<IJokeDto>> > GetMostPopularJokes (CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetMostPopularJokes (cancellationToken));
        }

        [HttpGet ("GetUserJokes/{mode}/{userEmail}/{jokesPerPage}/{pageIndex}", Name = "GetUserJokes")]
        public async Task<ActionResult<IReadOnlyList<IJokeDto>> > GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetUserJokes (mode, userEmail, jokesPerPage, pageIndex, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<IJokeDto>> Create (JObject jObject) {
            IJokeDto jokeDto = _jokeConverterService.ConvertJObjectToIokeDto (jObject);
            IJokeDto insertedJoke = await _jokeService.Create (jokeDto);

            return CreatedAtRoute ("GetJoke", new { id = insertedJoke.ToString () }, insertedJoke);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update (string id, JObject jObject) {
            try {
                IJokeDto jokeDto = _jokeConverterService.ConvertJObjectToIokeDto (jObject);
                await _jokeService.Update (id, jokeDto);
                return NoContent ();
            } catch (InvalidJokeIdException) {
                return BadRequest ();
            } catch (JokeNotFoundException) {
                return NotFound ();
            }

        }

        [HttpPut ("IncrementEmotionCounter/{id}/{emotion}")]
        public async Task<ActionResult<IJokeDto>> IncrementEmotionCounter (string id, string emotion) {
            try {
                IJokeDto updatedJoke = await _jokeService.IncrementEmotionCounter (id, emotion);
                return Ok (updatedJoke);
            } catch (JokeNotFoundException) {
                return BadRequest ();
            }
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (string id) {
            try {
                await _jokeService.Remove (id);
                return NoContent ();
            } catch (InvalidJokeIdException) {
                return BadRequest ();
            } catch (JokeNotFoundException) {
                return NotFound ();
            }
        }
    }

    class CustomResolver : DefaultContractResolver {
        protected override JsonObjectContract CreateObjectContract (Type objectType) {
            JsonObjectContract contract = base.CreateObjectContract (objectType);
            contract.DefaultCreator = () => {
                // Change this to use your IOC container to create the instance.
                var instance = Activator.CreateInstance (objectType);

                return instance;
            };
            return contract;
        }
    }

}