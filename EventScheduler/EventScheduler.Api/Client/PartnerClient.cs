using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EventScheduler.Api.Models;

namespace EventScheduler.Api.Client
{
  public interface IPartnerClient
  {
    public Task<PartnerAvailabilities> GetPartnerAvailabilities(CancellationToken ctx);
  }

  public class PartnerClient : IPartnerClient
  {
    // TODO: Move secret to appsettings
    private const string _uri = "dataset?userKey=c82cdf8b6a3e283c4d81169a6406";
    private readonly HttpClient _client;

    public PartnerClient(
      HttpClient client)
    {
      _client = client;
    }

    public async Task<PartnerAvailabilities> GetPartnerAvailabilities(CancellationToken ctx)
    {
      var response = await _client.GetAsync(_uri, ctx);
      response.EnsureSuccessStatusCode();

      return JsonSerializer.Deserialize<PartnerAvailabilities>(
        await response.Content.ReadAsStringAsync(ctx));
    }
  }
}
