using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleNlp
{
  public class EntitySpecification
  {
    private readonly EntityName _entityName;
    private readonly List<string> _patterns = new List<string>();

    public EntitySpecification(EntityName entityName)
    {
      _entityName = entityName;
    }

    public void ApplyTo(TokensUnderPreparation tokensUnderPreparation)
    {
      foreach (var pattern in _patterns)
      {
        tokensUnderPreparation.PartitionBasedOn(pattern);
      }
    }

    public void TryToMatch(string token, List<RecognizedEntity> recognizedEntities)
    {
      recognizedEntities.AddRange(
        from pattern in _patterns 
        where pattern.Equals(token, StringComparison.InvariantCultureIgnoreCase) 
        select RecognizedEntity.Value(_entityName, pattern));
    }

    public void AddPattern(string value)
    {
      _patterns.Add(value);
    }
  }
}