using System;

namespace BookStore.Services
{
  public class DBLogger : ILoggerService
  {
    public void Log(string message)
    {
      Console.WriteLine("[DBLogger] - " + message);
    }
  }
}