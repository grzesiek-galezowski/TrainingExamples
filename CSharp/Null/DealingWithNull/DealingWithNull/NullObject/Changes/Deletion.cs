using System;

namespace DealingWithNull.NullObject
{
  public class Deletion : AuthorizationChange
  {
    private readonly ChangeData _changeData;

    public Deletion(ChangeData changeData)
    {
      _changeData = changeData;
    }

    public void MakeTo(AuthorizationStructure authorizationStructure)
    {
      Console.WriteLine("Delete " + authorizationStructure);
    }
  }
}