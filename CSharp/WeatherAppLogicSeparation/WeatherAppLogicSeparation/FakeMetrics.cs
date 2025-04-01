using Application;

namespace WeatherAppLogicSeparation;

public class FakeMetrics : IMetrics
{
  public async Task ReportException(Exception exception)
  {
    //sorry ^_^
    Console.WriteLine(exception);
  }
}