using System;
using System.Collections.Generic;
using Ledger.Cli.Model;

namespace Ledger.Cli.Calculator
{
  public interface IEquityEventAggregator
  {
    /// <summary>
    /// Evaluates total number of vested positions for each employee on given target date.
    /// If an employee has no vested positions, they will still be present on list.
    /// </summary>
    /// <param name="equityEvents">All events to consider.</param>
    /// <param name="targetDate">Date for vest calculation.</param>
    /// <param name="precision">Decimal precision to consider when considering fractional shares.</param>
    /// <returns>Collection containing total number of shares for each employee.</returns>
    public IEnumerable<EquityPosition> GetAllEmployeePositions(
      IEnumerable<EquityEvent> equityEvents,
      DateTime targetDate,
      int precision);
  }
}
