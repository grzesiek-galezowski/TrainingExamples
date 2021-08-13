namespace DealingWithNull.NullObject
{
  public interface ChangeFactory
  {
    AuthorizationChange CreateOneDescribedBy(ChangeData changeData);
  }
}