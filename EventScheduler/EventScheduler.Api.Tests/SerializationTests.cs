using System;
using System.Collections.Generic;
using System.Text.Json;
using EventScheduler.Api.Models;
using Xunit;

namespace EventScheduler.Api.Tests
{
  public class SerializationTests
  {
    [Fact]
    public void TestPartnerAvailablityDeserialization()
    {
      var expected = new PartnerAvailabilities
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
      };

      var responseBody = @"
{
  ""partners"": [
    {
      ""firstName"": ""Darin"",
      ""lastName"": ""Daignault"",
      ""email"": ""ddaignault@hubspotpartners.com"",
      ""country"": ""United States"",
      ""availableDates"": [
      ""2017-05-03"",
      ""2017-05-06""
      ]
    },
    {
      ""firstName"": ""Crystal"",
      ""lastName"": ""Brenna"",
      ""email"": ""cbrenna@hubspotpartners.com"",
      ""country"": ""Ireland"",
      ""availableDates"": [
      ""2017-04-27"",
      ""2017-04-29"",
      ""2017-04-30""
      ]
    },
    {
      ""firstName"": ""Janyce"",
      ""lastName"": ""Gustison"",
      ""email"": ""jgustison@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-29"",
      ""2017-04-30"",
      ""2017-05-01""
      ]
    },
    {
      ""firstName"": ""Tifany"",
      ""lastName"": ""Mozie"",
      ""email"": ""tmozie@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-28"",
      ""2017-04-29"",
      ""2017-05-01"",
      ""2017-05-04""
      ]
    },
    {
      ""firstName"": ""Temple"",
      ""lastName"": ""Affelt"",
      ""email"": ""taffelt@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-28"",
      ""2017-04-29"",
      ""2017-05-02"",
      ""2017-05-04""
      ]
    },
    {
      ""firstName"": ""Robyn"",
      ""lastName"": ""Yarwood"",
      ""email"": ""ryarwood@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-29"",
      ""2017-04-30"",
      ""2017-05-02"",
      ""2017-05-03""
      ]
    },
    {
      ""firstName"": ""Shirlene"",
      ""lastName"": ""Filipponi"",
      ""email"": ""sfilipponi@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-30"",
      ""2017-05-01""
      ]
    },
    {
      ""firstName"": ""Oliver"",
      ""lastName"": ""Majica"",
      ""email"": ""omajica@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-28"",
      ""2017-04-29"",
      ""2017-05-01"",
      ""2017-05-03""
      ]
    },
    {
      ""firstName"": ""Wilber"",
      ""lastName"": ""Zartman"",
      ""email"": ""wzartman@hubspotpartners.com"",
      ""country"": ""Spain"",
      ""availableDates"": [
      ""2017-04-29"",
      ""2017-04-30"",
      ""2017-05-02"",
      ""2017-05-03""
      ]
    },
    {
      ""firstName"": ""Eugena"",
      ""lastName"": ""Auther"",
      ""email"": ""eauther@hubspotpartners.com"",
      ""country"": ""United States"",
      ""availableDates"": [
      ""2017-05-04"",
      ""2017-05-09""
      ]
    }
  ]
}";

      var actual = JsonSerializer.Deserialize<PartnerAvailabilities>(responseBody);

      Assert.Equal(expected.Availabilities, actual.Availabilities);
    }
  }
}
