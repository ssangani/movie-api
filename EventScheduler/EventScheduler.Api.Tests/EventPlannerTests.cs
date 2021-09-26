using System;
using System.Collections.Generic;
using EventScheduler.Api.Calculators;
using EventScheduler.Api.Models;
using Xunit;

namespace EventScheduler.Api.Tests
{
  public class EventPlannerTests
  {
    private readonly EventPlanner _sut = new EventPlanner();

    [Theory]
    [MemberData(nameof(ScheduleTestCase))]
    public void TestEventScheduling(EventSchedule expected, PartnerAvailabilities availabilities)
    {
      var actual = _sut.Schedule(availabilities);
      foreach (var countryEvent in actual.Events)
      {
        Assert.Contains(countryEvent, expected.Events);
      }
    }

    public static IEnumerable<object[]> ScheduleTestCase()
    {
      yield return new object[]
      {
        new EventSchedule {
          Events = new List<CountryEvent>
          {
            new CountryEvent
            {
              Attendees = new HashSet<string>
              {
                "cbrenna@hubspotpartners.com"
              },
              Name = "Ireland",
              EventDate = new DateTime(2017,4,29)
            },
            new CountryEvent
            {
              Attendees = new HashSet<string>(),
              Name = "United States",
              EventDate = null
            },
            new CountryEvent
            {
              Attendees = new HashSet<string> {
                "omajica@hubspotpartners.com",
                "taffelt@hubspotpartners.com",
                "tmozie@hubspotpartners.com"
              },
              Name = "Spain",
              EventDate = new DateTime(2017,4,28)
            }
          }
        },
        new PartnerAvailabilities
        {
          Availabilities = new List<PartnerAvailability>
          {
            new PartnerAvailability()
            {
              FirstName = "Darin",
              LastName = "Daignault",
              Email = "ddaignault@hubspotpartners.com",
              Country = "United States",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 5, 3),
                new DateTime(2017, 5, 6)

              }
            },
            new PartnerAvailability()
            {
              FirstName = "Crystal",
              LastName = "Brenna",
              Email = "cbrenna@hubspotpartners.com",
              Country = "Ireland",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 27),
                new DateTime(2017, 4, 29),
                new DateTime(2017, 4, 30)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Janyce",
              LastName = "Gustison",
              Email = "jgustison@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 29),
                new DateTime(2017, 4, 30),
                new DateTime(2017, 5, 1)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Tifany",
              LastName = "Mozie",
              Email = "tmozie@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 28),
                new DateTime(2017, 4, 29),
                new DateTime(2017, 5, 1),
                new DateTime(2017, 5, 4)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Temple",
              LastName = "Affelt",
              Email = "taffelt@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 28),
                new DateTime(2017, 4, 29),
                new DateTime(2017, 5, 2),
                new DateTime(2017, 5, 4)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Robyn",
              LastName = "Yarwood",
              Email = "ryarwood@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 29),
                new DateTime(2017, 4, 30),
                new DateTime(2017, 5, 2),
                new DateTime(2017, 5, 3)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Shirlene",
              LastName = "Filipponi",
              Email = "sfilipponi@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 30),
                new DateTime(2017, 5, 1)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Oliver",
              LastName = "Majica",
              Email = "omajica@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 28),
                new DateTime(2017, 4, 29),
                new DateTime(2017, 5, 1),
                new DateTime(2017, 5, 3)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Wilber",
              LastName = "Zartman",
              Email = "wzartman@hubspotpartners.com",
              Country = "Spain",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 4, 29),
                new DateTime(2017, 4, 30),
                new DateTime(2017, 5, 2),
                new DateTime(2017, 5, 3)
              }
            },
            new PartnerAvailability()
            {
              FirstName = "Eugena",
              LastName = "Auther",
              Email = "eauther@hubspotpartners.com",
              Country = "United States",
              AvailableDates = new HashSet<DateTime>
              {
                new DateTime(2017, 5, 4),
                new DateTime(2017, 5, 9)
              }
            }
          }
        }
      };
    }
  }
}
