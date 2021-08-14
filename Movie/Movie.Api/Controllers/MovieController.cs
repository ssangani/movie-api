using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Engine.Models;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpGet("find")]
        [ProducesResponseType(typeof(MovieInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieInfo[]>> Find(
            [FromQuery] string titleLike,
            [FromQuery] int yearOfRelease,
            [FromQuery] string[] genres)
        {
            try
            {
                return NotFound();
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
            [FromQuery] int userId)
        {
            try
            {
                return NotFound();
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
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
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
