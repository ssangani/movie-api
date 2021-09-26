using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventScheduler.Api.Models
{
  public class PartnerAvailabilities
  {
    [JsonPropertyName("partners")]
    public IList<PartnerAvailability> Availabilities { get; set; }
  }
}
