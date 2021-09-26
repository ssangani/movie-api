using System.Threading;
using System.Threading.Tasks;
using EventScheduler.Api.Calculators;
using EventScheduler.Api.Client;
using Microsoft.AspNetCore.Mvc;

namespace EventScheduler.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventSchedulerController : ControllerBase
  {
    private readonly IEventSchedulerClient _eventSchedulerClient;
    private readonly IPartnerClient _partnerClient;
    private readonly IEventPlanner _eventPlanner;

    public EventSchedulerController(
      IEventSchedulerClient eventSchedulerClient,
      IPartnerClient partnerClient,
      IEventPlanner eventPlanner)
    {
      _eventSchedulerClient = eventSchedulerClient;
      _partnerClient = partnerClient;
      _eventPlanner = eventPlanner;
    }

    [HttpPost]
    public async Task ScheduleAsync(CancellationToken ctx)
    {
      var availabilities = await _partnerClient.GetPartnerAvailabilities(ctx);
      var schedule = _eventPlanner.Schedule(availabilities);
      await _eventSchedulerClient.ScheduleAsync(schedule, ctx);
    }
  }
}
