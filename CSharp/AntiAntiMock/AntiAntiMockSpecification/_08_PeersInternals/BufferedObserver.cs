namespace MockNoMockSpecification._08_PeersInternals;

public class BufferedObserver : IMyObserver
{
  private readonly IMyObserver _observer;
  private List<IMyEvent> _events = new List<IMyEvent>();

  public BufferedObserver(IMyObserver observer)
  {
    _observer = observer;
  }

  public void Send(params IMyEvent[] events)
  {
    _events.AddRange(events);
  }

  public void Flush()
  {
    _observer.Send(_events.ToArray());
    _events = new List<IMyEvent>();
  }
}