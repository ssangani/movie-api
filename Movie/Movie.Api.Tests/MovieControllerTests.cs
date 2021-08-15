using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Movie.Engine.Models;
using Xunit;

namespace Movie.Api.Tests
{
    public class MovieControllerTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MovieControllerTests(
            WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Find_WhenYearOfReleaseInvalid_ThenNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/Movie/find?yearOfRelease=202020");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Find_WhenNoParametersProvided_ThenBadRequest()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/Movie/find");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(1936, "My Man Godfrey")]
        [InlineData(1965, "Doctor Zhivago")]
        public async Task Find_WhenYearOfReleaseMatches_ThenVerifyMovieIsWithinOutput(int year, string expectedMovie)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/Movie/find?yearOfRelease={year}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<MovieInfo[]>(body);
            Assert.Contains(movies, movie => movie.Title == expectedMovie);
        }

        [Theory]
        [InlineData("My Man", "My Man Godfrey")]
        [InlineData("Doctor Zhivago", "Doctor Zhivago")]
        [InlineData("Mr.", "Mr. Smith Goes to Washington")]
        [InlineData("Mr.", "Mr. Deeds Goes to Town")]
        public async Task Find_WhenTitleMatchesFullyOrPartially_ThenVerifyOutput(string titleLike, string expectedMovie)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"api/Movie/find?titleLike={titleLike}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<MovieInfo[]>(body);
            Assert.Contains(movies, movie => movie.Title.Equals(expectedMovie, System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Theory]
        [InlineData(new[] { "Noir" }, "The Big Sleep")]
        [InlineData(new[] { "Drama" }, "The Treasure of the Sierra Madre")]
        [InlineData(new[] { "romance" }, "Casablanca")]
        public async Task Find_WhenGenreMatches_ThenMatchExpectedMovie(string[] genres, string expectedMovie)
        {
            var client = _factory.CreateClient();

            var path = new StringBuilder("api/Movie/find?");
            foreach (var genre in genres)
                path.Append($"&genres={genre}");

            var response = await client.GetAsync(path.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<MovieInfo[]>(body);
            Assert.Contains(movies, movie => movie.Title.Equals(expectedMovie, System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Theory]
        [InlineData("Breakfast At Tiffany's", 1961, new [] {"comedy", "romance"}, "Breakfast At Tiffany's")]
        public async Task Find_WhenUniqueQuery_ThenVerifySingleOutput(
            string titleLike,
            int yearOfRelease,
            string[] genres,
            string expectedMovie)
        {
            var client = _factory.CreateClient();

            var path = new StringBuilder("api/Movie/find?");
            path.Append($"&titleLike={titleLike}");
            path.Append($"&yearOfRelease={yearOfRelease}");
            foreach (var genre in genres)
                path.Append($"&genres={genre}");

            var response = await client.GetAsync(path.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<MovieInfo[]>(body);
            Assert.Single(movies);
            Assert.Contains(movies, movie => movie.Title.Equals(expectedMovie, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
