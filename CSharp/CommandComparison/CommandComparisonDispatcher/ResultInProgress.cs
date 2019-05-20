using System;

namespace CommandComparisonDispatcher
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