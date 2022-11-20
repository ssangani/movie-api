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
}
