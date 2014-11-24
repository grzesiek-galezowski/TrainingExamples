namespace DealingWithNull.NullObject
{
  public class IncomingChangeProcessing
  {
    private readonly ChangeFactory _changeFactory;
    private readonly AuthorizationStructure _authorizationStructure;

    public IncomingChangeProcessing(ChangeFactory changeFactory, AuthorizationStructure authorizationStructure)
    {
      _changeFactory = changeFactory;
      _authorizationStructure = authorizationStructure;
    }

    public void PerformFor(ChangeData changeData)
    {
      var change = _changeFactory.CreateOneDescribedBy(changeData);

      if (change != null)
      {
        change.MakeTo(_authorizationStructure);
      }
    }
  }
}
