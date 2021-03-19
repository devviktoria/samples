using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Models.Dtos;
using jcwebapi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace jcwebapi.Controllers {

    //[EnableCors ("_JesterClubSpecificOrigins")]
    [Route ("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase {
        private readonly JokeService _jokeService;

        public JokeController (JokeService jokeService) {
            _jokeService = jokeService;
        }

        [HttpGet ("{id}", Name = "GetJoke")]
        public async Task<ActionResult<JokeDto>> Get (string id) {
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

        [HttpGet ("GetUserJokes/{mode}/{userEmail}/{jokesPerPage}/{pageIndex}", Name = "GetUserJokes")]
        public async Task<ActionResult<IReadOnlyList<Joke>> > GetUserJokes (string mode, string userEmail, int jokesPerPage, int pageIndex, CancellationToken cancellationToken) {
            return Ok (await _jokeService.GetUserJokes (mode, userEmail, jokesPerPage, pageIndex, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<Joke>> Create (JokeDto joke) {
            JokeDto insertedJoke = await _jokeService.Create (joke);

            return CreatedAtRoute ("GetJoke", new { id = insertedJoke.ToString () }, insertedJoke);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update (string id, JokeDto jokeIn) {
            try {
                await _jokeService.Update (id, jokeIn);
                return NoContent ();
            } catch (InvalidJokeIdException) {
                return BadRequest ();
            } catch (JokeNotFoundException) {
                return NotFound ();
            }

        }

        [HttpPut ("IncrementEmotionCounter/{id}/{emotion}")]
        public async Task<ActionResult<JokeDto>> IncrementEmotionCounter (string id, string emotion) {
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
}