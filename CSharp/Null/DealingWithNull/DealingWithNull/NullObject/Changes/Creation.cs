using System;

namespace DealingWithNull.NullObject
{
  public class Creation : AuthorizationChange
  {
    private readonly ChangeData _changeData;

    public Creation(ChangeData changeData)
    {
      _changeData = changeData;
    }

    public void MakeTo(AuthorizationStructure authorizationStructure)
    {
      Console.WriteLine("Create " + authorizationStructure);
    }
  }
}