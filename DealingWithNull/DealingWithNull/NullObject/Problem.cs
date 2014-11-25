namespace DealingWithNull.NullObject
{
  public class Problem
  {
    public void Main()
    {
      var x = new IncomingChangeProcessing(new AuthorizationChangeFactory(), new MyApplicationAuthorizationStructure());

      x.PerformFor(new ChangeData() { ChangeType = ChangeTypes.Update });
    }
  }
}