using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.Models;

namespace Ledger.Engine.DataAccess
{
  public interface IEquityDataStore
  {
    public Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default);
  }
}
