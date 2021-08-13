namespace DealingWithNull.NullObject
{
  public class AuthorizationChangeFactory : ChangeFactory
  {
    public AuthorizationChange CreateOneDescribedBy(ChangeData changeData)
    {
      if (changeData.ChangeType == ChangeTypes.Create)
      {
        return new Creation(changeData);
      }
      else if (changeData.ChangeType == ChangeTypes.Update)
      {
        return new Update(changeData);
      }
      else if (changeData.ChangeType == ChangeTypes.Delete)
      {
        return new Deletion(changeData);
      }

      return null;
    }
  }
}