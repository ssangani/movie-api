using System;

namespace Ledger.Cli.Service
{
  public class LedgerCommand
  {
    public string ImportPath { get; set; }
    public DateTime TargetDate { get; set; } = DateTime.Now;
  }
}
