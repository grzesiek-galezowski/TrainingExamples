namespace CommandComparisonFactory
{
  public class ResultInProgressFactory : IResultInProgressFactory
  {
    public ResultInProgress CreateResultInProgress()
    {
      return new ResultInProgress();
    }
  }
}