using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ledger.Cli
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello");

      CreateHostBuilder(args)
        .Build()
        .Run();

      Console.WriteLine("Bye");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
