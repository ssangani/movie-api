using System;
using System.Collections.Generic;
using System.Linq;
using EventScheduler.Api.Models;

namespace EventScheduler.Api.Calculators
{
  public class EventPlanner : IEventPlanner
  {
    public EventSchedule Schedule(PartnerAvailabilities partnerAvailabilities)
    {
      var res = new EventSchedule
      {
        Events = new List<CountryEvent>()
      };

      var partnersByCountry = new Dictionary<string, HashSet<int>>();
      for (var i = 0; i < partnerAvailabilities.Availabilities.Count; i++)
      {
        var partner = partnerAvailabilities.Availabilities[i];
        if (partnersByCountry.TryGetValue(partner.Country, out var partnerSet)) {
          partnerSet.Add(i);
        } else
        {
          partnersByCountry.Add(partner.Country, new HashSet<int> { i });
        }
      }

      foreach (var countryPartners in partnersByCountry)
      {
        var attendeesByAvailability = new Dictionary<DateTime, HashSet<int>>();
        foreach (var partnerIdx in countryPartners.Value)
        {
          var availableDates = partnerAvailabilities
            .Availabilities[partnerIdx]
            .AvailableDates
            .ToArray();

          Array.Sort(availableDates);

          for (var j = 0; j < availableDates.Length - 1; j++)
          {
            // If there are two consecutive days when partner is available
            // then that's one of the available start dates for that partner
            if (availableDates[j].AddDays(1).Equals(availableDates[j + 1]))
            {
              if (attendeesByAvailability.TryGetValue(availableDates[j], out var partnerSet)) {
                partnerSet.Add(partnerIdx);
              }
              else
              {
                attendeesByAvailability.Add(availableDates[j], new HashSet<int> { partnerIdx });
              }
            }
          }
        }

        // Since there are no attendees available to attend two-day event, we add an empty placeholder
        if (attendeesByAvailability.Count == 0)
        {
          res.Events.Add(
            new CountryEvent
            {
              Name = countryPartners.Key,
              Attendees = new HashSet<string>(),
              StartDate = null,
            });
          continue;
        }

        DateTime earliestEventDate = default;
        int maxAttendeeCount = 0;
        foreach (var eventDateAttendeePairs in attendeesByAvailability)
        {
          // Find start date with most attendees. If there are multiple, choose earliest one
          if (earliestEventDate == default ||
            eventDateAttendeePairs.Value.Count > maxAttendeeCount ||
            (eventDateAttendeePairs.Value.Count == maxAttendeeCount && eventDateAttendeePairs.Key < earliestEventDate))
          {
            earliestEventDate = eventDateAttendeePairs.Key;
            maxAttendeeCount = eventDateAttendeePairs.Value.Count;
          }
        }

        var attendees = attendeesByAvailability[earliestEventDate]
          .Select(partnerIdx => partnerAvailabilities.Availabilities[partnerIdx].Email)
          .ToHashSet();

        res.Events.Add(
            new CountryEvent
            {
              Name = countryPartners.Key,
              Attendees = attendees,
              StartDate = earliestEventDate,
            });
      }

      return res;
    }
  }
}
