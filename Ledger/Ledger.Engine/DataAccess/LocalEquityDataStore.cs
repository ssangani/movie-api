using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.Models;

namespace Ledger.Engine.DataAccess
{
  public class LocalEquityDataStore : IEquityDataStore
  {
    private readonly BlockingCollection<EquityEvent> _events;

    public LocalEquityDataStore()
    {
      _events = new BlockingCollection<EquityEvent>();
    }

    public async Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default)
    {
      await Task.Yield();
      _events.Add(equityEvent, ctx);
    }
  }
}
