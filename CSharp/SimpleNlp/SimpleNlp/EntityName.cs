using System.Collections.Generic;
using Value;

namespace TddXt.SimpleNlp
{
  public class EntityName : ValueType<EntityName>
  {
    public static EntityName Value(string name)
    {
      return new EntityName(name);
    }

    private readonly string _name;

    public EntityName(string name)
    {
      _name = name;
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
      yield return _name;
    }

    public override string ToString()
    {
      return _name;
    }
  }
}