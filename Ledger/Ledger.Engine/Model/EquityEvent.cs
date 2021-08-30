using System;
using System.Text;

namespace Ledger.Engine.Model
{
  public class EquityEvent
  {
    public Guid Id { get; set; }
    public EquityEventType Type { get; set; }
    public Employee Employee { get; set; }
    public string AwardId { get; set; }
    public DateTime GrantDate { get; set; }
    public decimal Quantity { get; set; }
    public override string ToString() => new StringBuilder()
      .Append(Type)
      .Append('|')
      .Append(Employee.Id)
      .Append('|')
      .Append(Employee.Name)
      .Append('|')
      .Append(AwardId)
      .Append('|')
      .Append(GrantDate)
      .Append('|')
      .Append(Quantity)
      .ToString();
  }
}
