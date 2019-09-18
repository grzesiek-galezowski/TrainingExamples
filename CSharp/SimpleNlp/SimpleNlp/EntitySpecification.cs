using System;
using System.Collections.Generic;

namespace SimpleNlp
{
  public class EntitySpecification
  {
    private readonly EntityName _entityName;
    private readonly string _pattern;

    public EntitySpecification(EntityName entityName, string pattern)
    {
      _entityName = entityName;
      _pattern = pattern;
    }

    public void ApplyTo(TokensUnderPreparation tokensUnderPreparation)
    {
      tokensUnderPreparation.PartitionBasedOn(_pattern);
    }

    public void TryToMatch(string token, List<RecognizedEntity> recognizedEntities)
    {
      if (_pattern.Equals(token, StringComparison.InvariantCultureIgnoreCase))
      {
        recognizedEntities.Add(new RecognizedEntity(_entityName, _pattern));
      }
    }
  }
}