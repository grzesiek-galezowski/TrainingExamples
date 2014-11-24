using System;

namespace DealingWithNull.NullObject
{
  public class Update : AuthorizationChange
  {
    private readonly ChangeData _changeData;

    public Update(ChangeData changeData)
    {
      _changeData = changeData;
    }

    public void MakeTo(AuthorizationStructure authorizationStructure)
    {
      Console.WriteLine("Update " + authorizationStructure);
    }
  }
}