using AtmaFileSystem;

namespace Application.Ports;

public interface IPatternLoadingObserver
{
  Task PatternLoaded(SequenceDto sequence, CancellationToken cancellationToken);
}

public interface IPatternSavingObserver
{
  void PatternSaved(AbsoluteFilePath filePath);
}