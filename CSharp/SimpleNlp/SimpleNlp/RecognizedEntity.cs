using System.Collections.Generic;
using Value;

namespace TddXt.SimpleNlp
{
  public class RecognizedEntity : ValueType<RecognizedEntity>
  {
    private readonly string _recognizedValue;

    public RecognizedEntity(EntityName entityName, string recognizedValue)
    {
      Entity = entityName;
      _recognizedValue = recognizedValue;
    }

    public EntityName Entity { get; }

    public static RecognizedEntity Value(EntityName name, string recognizedValue)
    {
      return new RecognizedEntity(name, recognizedValue);
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
      yield return Entity;
      yield return _recognizedValue;
    }

    public override string ToString()
    {
      return $"{nameof(Entity)}: {Entity}, {nameof(_recognizedValue)}: {_recognizedValue}";
    }
  }
}