using Application.Ports;

namespace Application;

public class BroadcastingOctaveObserver : IOctaveObserver
{
  private readonly List<IOctaveObserver> _observers;

  public BroadcastingOctaveObserver(List<IOctaveObserver> observers)
  {
    _observers = observers;
  }

  public void OnOctaveChanged(Tb03Octave newOctave)
  {
    foreach (var octaveObserver in _observers)
    {
      octaveObserver.OnOctaveChanged(newOctave);
    }
  }
}