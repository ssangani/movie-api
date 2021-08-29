using System;

namespace Ledger.Engine.Models
{
  public class EquityEvent
  {
    public Guid Id { get; set; }
    public EquityEventType Type { get; set; }
    public Employee Employee { get; set; }
    public string AwardId { get; set; }
    public DateTime GrantDate { get; set; }
    public decimal Quantity { get; set; }
  }
}
