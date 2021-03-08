using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace jcwebapi.Controllers {

    [EnableCors ("_JesterClubSpecificOrigins")]
    [Route ("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase {
        private readonly JokeService _jokeService;

        public JokeController (JokeService jokeService) {
            _jokeService = jokeService;
        }

        [HttpGet ("{id:length(24)}", Name = "GetJoke")]
        public async Task<ActionResult<Joke>> Get (string id) {
            var joke = await _jokeService.Get (id);

            if (joke == null) {
                return NotFound ();
            }

            return joke;
        }

        [HttpGet (nameof (GetLatestJokes), Name = nameof (GetLatestJokes))]
        public async Task<ActionResult<IReadOnlyList<Joke>> > GetLatestJokes (CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetLatestJokes (cancellationToken));
        }

        [HttpGet (nameof (GetMostPopularJokes), Name = nameof (GetMostPopularJokes))]
        public async Task<ActionResult<IReadOnlyList<Joke>> > GetMostPopularJokes (CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetMostPopularJokes (cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<Joke>> Create (Joke joke) {
            await _jokeService.Create (joke);

            return CreatedAtRoute ("GetJoke", new { id = joke.Id.ToString () }, joke);
        }

        [HttpPut ("{id:length(24)}")]
        public async Task<IActionResult> Update (string id, Joke jokeIn) {
            var joke = await _jokeService.Get (id);

            if (joke == null) {
                return NotFound ();
            }

            await _jokeService.Update (id, jokeIn);

            return NoContent ();
        }

        [HttpPut ("IncrementEmotionCounter/{id:length(24)}/{emotion}")]
        public async Task<ActionResult<Joke>> IncrementEmotionCounter (string id, string emotion) {
            if (!Array.Exists (EmotionCounter.Emotions, e => e == emotion)) {
                return BadRequest ();
            }

            var joke = await _jokeService.Get (id);

            if (joke == null) {
                return NotFound ();
            }

            var updatedJoke = await _jokeService.IncrementEmotionCounter (id, emotion);

            return updatedJoke;
        }

        [HttpDelete ("{id:length(24)}")]
        public async Task<IActionResult> Delete (string id) {
            var joke = await _jokeService.Get (id);

            if (joke == null) {
                return NotFound ();
            }

            await _jokeService.Remove (joke.Id);

            return NoContent ();
        }
    }
}