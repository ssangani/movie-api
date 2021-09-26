using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventScheduler.Api.Models
{
  public class EventSchedule
  {
    [JsonPropertyName("countries")]
    public IList<CountryEvent> Events { get; set; }
  }
}
