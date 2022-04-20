namespace MockNoMockSpecification._08_PeersInternals;

public interface IMyObserver
{
  void Send(params IMyEvent[] events);
}