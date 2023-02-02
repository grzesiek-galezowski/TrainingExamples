namespace Observer.Common;

internal class WhateverCleanedUpDir : ICleanedUpDir
{
  public IReadOnlyCollection<ICleanedUpFile> GetFilesToCleanup()
  {
    throw new NotImplementedException();
  }
}