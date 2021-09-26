using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventScheduler.Api.Models
{
  public class CountryEvent
  {
    [JsonPropertyName("attendeeCount")]
    public int AttendeeCount => Attendees.Count;
    [JsonPropertyName("attendees")]
    public HashSet<string> Attendees { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("startDate")]
    public string StartDate => EventDate?.ToString("yyyy-MM-dd");

    public DateTime? EventDate { get; set; }

    public override bool Equals(object other)
    {
      if (other == null)
        return false;
      if (ReferenceEquals(this, other))
        return true;
      return other.GetType() == GetType() &&
             Equals((CountryEvent)other);
    }

    public bool Equals(CountryEvent other)
    {
      if (other == null)
        return false;
      if (ReferenceEquals(this, other))
        return true;

      return AttendeeCount == other.AttendeeCount &&
        Attendees.SetEquals(other.Attendees) &&
        Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase) &&
        EventDate.Equals(other.EventDate);
    }

    public override int GetHashCode()
    {
      var hash = new HashCode();

      hash.Add(AttendeeCount);
      hash.Add(Attendees);
      hash.Add(Name);
      hash.Add(EventDate);

      return hash.ToHashCode();
    }
  }
}
