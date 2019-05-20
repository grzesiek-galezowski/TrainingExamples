namespace CommandComparisonDispatcher
{
  public class ResultInProgressFactory : IResultInProgressFactory
  {
    public ResultInProgress CreateResultInProgress()
    {
      return new ResultInProgress();
    }
  }
}