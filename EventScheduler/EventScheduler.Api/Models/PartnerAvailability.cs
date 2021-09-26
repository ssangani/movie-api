using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventScheduler.Api.Models
{
  public class PartnerAvailability
  {
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
    [JsonPropertyName("availableDates")]
    public HashSet<DateTime> AvailableDates { get; set; }

    public override bool Equals(object other)
    {
      if (other == null)
        return false;
      if (ReferenceEquals(this, other))
        return true;
      return other.GetType() == GetType() &&
             Equals((PartnerAvailability) other);
    }

    public bool Equals(PartnerAvailability other)
    {
      if (other == null)
        return false;
      if (ReferenceEquals(this, other))
        return true;

      return FirstName.Equals(other.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
        LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase) &&
        Email.Equals(other.Email, StringComparison.InvariantCultureIgnoreCase) &&
        Country.Equals(other.Country, StringComparison.InvariantCultureIgnoreCase) &&
        AvailableDates.SetEquals(other.AvailableDates);
    }

    public override int GetHashCode()
    {
      var hash = new HashCode();

      hash.Add(FirstName);
      hash.Add(LastName);
      hash.Add(Email);
      hash.Add(Country);
      hash.Add(AvailableDates);

      return hash.ToHashCode();
    }
  }
}
