﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ledger.Engine.Model;

namespace Ledger.Engine.Calculator
{
  public interface IEquityEventAggregator
  {
    public IEnumerable<EquityPosition> GetAllEmployeePositions(
      IEnumerable<EquityEvent> equityEvents,
      DateTime targetDate);
  }

  public class EquityEventAggregator : IEquityEventAggregator
  {
    private const int FractionalSharePrecision = 6;

    public IEnumerable<EquityPosition> GetAllEmployeePositions(
      IEnumerable<EquityEvent> equityEvents,
      DateTime targetDate)
    {
      return equityEvents
        .GroupBy(ee => ee.Employee.Id)
        .SelectMany(ee => GetEmployeePositions(ee, targetDate))
        .OrderBy(pos => pos.Employee.Id)
        .OrderBy(pos => pos.AwardId);
    }

    private IEnumerable<EquityPosition> GetEmployeePositions(
      IEnumerable<EquityEvent> equityEvents,
      DateTime targetDate)
    {
      // We will assume last most entry has latest employee name
      var employee = equityEvents.Last().Employee;

      // Aggregate
      var awards = new Dictionary<string, decimal>();
      foreach (var equityEvent in equityEvents)
      {
        if (!awards.ContainsKey(equityEvent.AwardId))
        {
          awards.Add(equityEvent.AwardId, 0);
        }

        if (equityEvent.GrantDate.Date <= targetDate.Date)
        {
          var sanitizedQuantity = SanitizeFractionalShares(equityEvent.Quantity);
          if (equityEvent.Type == EquityEventType.VEST)
          {
            // Add to vested quantity
            awards[equityEvent.AwardId] += sanitizedQuantity;
          }
          else if (equityEvent.Type == EquityEventType.CANCEL && awards[equityEvent.AwardId] >= sanitizedQuantity)
          {
            // Subtract only if total vested amount is higher or same as cancellation amount
            awards[equityEvent.AwardId] -= sanitizedQuantity;
          }
        }
      }

      // Return positions for all awards
      foreach (var award in awards)
      {
        yield return new EquityPosition
        {
          Employee = employee,
          AwardId = award.Key,
          Quantity = SanitizeFractionalShares(award.Value)
        };
      }
    }

    private decimal SanitizeFractionalShares(decimal quantity) => Math.Round(quantity, FractionalSharePrecision);
  }
}
