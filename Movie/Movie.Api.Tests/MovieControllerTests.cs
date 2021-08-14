using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
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
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
