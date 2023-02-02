namespace Observer.Common;

internal class WhateverCleanedUpProcedure : ICleanUpProcedure
{
  public void RunOn(IReadOnlyCollection<ICleanedUpFile> files)
  {
    throw new NotImplementedException();
  }
}