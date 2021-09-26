using EventScheduler.Api.Models;

namespace EventScheduler.Api.Calculators
{
  public interface IEventPlanner
  {
    /// <summary>
    /// Schedule a two day event for each country based on given set of partner availabilities.
    /// 1. If none of the partners from a country can attend for two day event, generate an empty country event
    /// 2. If there are multiple days during which partners can attend a two day event then choose
    ///    the earliest date when maximum number of partners can attend, and include them in attendees list
    /// </summary>
    /// <param name="partnerAvailabilities"></param>
    /// <returns></returns>
    public EventSchedule Schedule(PartnerAvailabilities partnerAvailabilities);
  }
}
