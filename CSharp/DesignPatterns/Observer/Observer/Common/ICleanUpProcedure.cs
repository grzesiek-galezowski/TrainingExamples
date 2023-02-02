namespace Observer.Common;

public interface ICleanUpProcedure
{
  void RunOn(IReadOnlyCollection<ICleanedUpFile> files);
}