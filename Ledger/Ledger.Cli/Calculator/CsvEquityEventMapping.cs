using Ledger.Cli.Model;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace Ledger.Cli.Calculator
{
  public class CsvEquityEventMapping : CsvMapping<EquityEvent>
  {
    public CsvEquityEventMapping()
      : base()
    {
      MapProperty(0, x => x.Type, new EnumConverter<EquityEventType>(true));
      MapUsing((entity, values) =>
      {
        if (values.Tokens.Length < 3)
        {
          return false;
        }

        entity.Employee = new Employee
        {
          Id = values.Tokens[1],
          Name = values.Tokens[2]
        };

        return true;
      });
      MapProperty(3, x => x.AwardId);
      MapProperty(4, x => x.GrantDate);
      MapProperty(5, x => x.Quantity);
    }
  }
}
