using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EventScheduler.Api.Models;

namespace EventScheduler.Api.Client
{
  public interface IEventSchedulerClient
  {
    public Task ScheduleAsync(
      EventSchedule schedule,
      CancellationToken ctx);
  }
  public class EventSchedulerClient : IEventSchedulerClient
  {
    // TODO: Move secret to appsettings
    private const string _uri = "result?userKey=c82cdf8b6a3e283c4d81169a6406";
    private readonly HttpClient _client;

    public EventSchedulerClient(
      HttpClient client)
    {
      _client = client;
    }

    public async Task ScheduleAsync(
      EventSchedule schedule,
      CancellationToken ctx)
    {
      var responseBody = JsonSerializer.Serialize(schedule);
      var content = new StringContent(
        responseBody,
        Encoding.UTF8,
        MediaTypeNames.Application.Json);

      var response = await _client.PostAsync(_uri, content, ctx);
      response.EnsureSuccessStatusCode();
    }
  }
}
