using System.Collections.Generic;
using System.Threading.Tasks;
using jcwebapi.Models;
using jcwebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace jcwebapi.Controllers {
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