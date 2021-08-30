using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.Model;

namespace Ledger.Engine.DataAccess
{
  public interface IEquityDataStore
  {
    public Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default);

    public Task<IEnumerable<EquityEvent>> GetAsync(CancellationToken ctx = default);
  }
}
