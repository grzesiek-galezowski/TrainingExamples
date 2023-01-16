namespace Application.Ports;

public interface IPatternNotesObserver
{
  void PatternLoaded(SequenceDto sequence);
}