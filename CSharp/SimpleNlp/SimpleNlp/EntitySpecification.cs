using System;
using System.Collections.Generic;

namespace TddXt.SimpleNlp
{
  public class EntitySpecification
  {
    private readonly EntityName _entityName;
    private readonly string _pattern;
    private readonly string[] _synonyms;

    public EntitySpecification(EntityName entityName, string pattern, string[] synonyms)
    {
      _entityName = entityName;
      _pattern = pattern;
      _synonyms = synonyms;
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

      foreach (var synonym in _synonyms)
      {
        if (synonym.Equals(token, StringComparison.InvariantCultureIgnoreCase))
        {
          recognizedEntities.Add(new RecognizedEntity(_entityName, _pattern));
        }
      }
    }
  }
}