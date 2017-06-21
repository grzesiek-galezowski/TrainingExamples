using System;

namespace EventAnomalies.Example2
{
  public class Nonsense2 : IDisposable
  {
    private readonly IErrorService _service;
    private event ErrorHandling SomethingHappened;

    public Nonsense2(IErrorService service)
    {
      _service = service;
      this.SomethingHappened += _service.LogError;
    }

    public void Process(int argument)
    {
      if (argument != 1)
      {
        this.SomethingHappened("someone is trying to cheat on me!");
      }
    }

    public void Dispose()
    {
      this.SomethingHappened -= _service.LogError;
    }
  }
}
