using System.Text;

namespace Ledger.Engine.Model
{
  public class EquityPosition
  {
    public string Format { get; set; }
    public Employee Employee { get; set; }
    public string AwardId { get; set; }
    public decimal Quantity { get; set; }
    public override string ToString() => new StringBuilder()
        .Append(Employee.Id)
        .Append(',')
        .Append(Employee.Name)
        .Append(',')
        .Append(AwardId)
        .Append(',')
        .Append(Quantity.ToString(Format))
        .ToString();
  }
}
