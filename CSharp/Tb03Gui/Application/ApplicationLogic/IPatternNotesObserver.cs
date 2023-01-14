namespace Application.ApplicationLogic;

public interface IPatternNotesObserver
{
  void PatternLoaded(SequenceDto sequence);
}