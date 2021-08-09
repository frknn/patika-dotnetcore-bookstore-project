using System;

namespace BookStore.Services
{
  public class ConsoleLogger : ILoggerService
  {
    public void Log(string message)
    {
      Console.WriteLine("[ConsoleLogger] - " + message);
    }
  }
}