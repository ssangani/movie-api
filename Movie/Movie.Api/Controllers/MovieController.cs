using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Engine;
using Movie.Engine.Models;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieEngine _engine;

        public MovieController(
            IMovieEngine engine)
        {
            _engine = engine;
        }

        [HttpGet("find")]
        [ProducesResponseType(typeof(MovieInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieInfo[]>> Find(
            [FromQuery] string titleLike,
            [FromQuery] int? yearOfRelease,
            [FromQuery] string[] genres)
        {
            try
            {
                var result = (await _engine.GetAsync(titleLike, yearOfRelease, genres)).ToArray();
                if (result.Length < 1)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (ArgumentException ae)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ae.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("top")]
        [ProducesResponseType(typeof(MovieInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieInfo[]>> FindTopRated(
            [FromQuery] int? userId)
        {
            try
            {
                var result = (await _engine.GetTopRatedAsync(userId)).ToArray();
                if (result.Length < 1)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (ArgumentException ae)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ae.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("user/{userId}/title/{titleId}/{rating}")]
        public async Task<IActionResult> UpsertUserRating(
            int userId,
            int titleId,
            int rating)
        {
            try
            {
                var success = await _engine.PutAsync(userId, titleId, rating);
                return success ? Ok() : NotFound();
            }
            catch (ArgumentException ae)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ae.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
