using System;

namespace CommandComparisonFactory
{
  public class ResultInProgress
  {
    public Result CreateResult()
    {
      return new Result();
    }

    public void FailedForWhateverReason(Exception exception)
    {
      
    }

    public void SuccessfullyAdded(string name, string surname)
    {
      
    }
  }
}