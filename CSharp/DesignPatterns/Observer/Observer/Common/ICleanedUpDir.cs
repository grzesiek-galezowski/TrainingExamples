namespace Observer.Common;

public interface ICleanedUpDir
{
  IReadOnlyCollection<ICleanedUpFile> GetFilesToCleanup();
}