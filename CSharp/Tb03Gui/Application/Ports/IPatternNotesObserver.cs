namespace Application.Ports;

public interface IPatternNotesObserver
{
  Task PatternLoaded(SequenceDto sequence, CancellationToken cancellationToken);
}