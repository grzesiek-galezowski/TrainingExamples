namespace Application.Ports;

public interface IOctaveObserver
{
  void OnOctaveChanged(Tb03Octave newOctave);
}