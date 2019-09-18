using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleNlp
{
  public class Model
  {
    private const string IntentNone = "None";
    private readonly Dictionary<EntityName, EntitySpecification> _entitySpecifications = new Dictionary<EntityName, EntitySpecification>();
    private IntentSpecification _intentSpecification;

    public void AddEntity(EntityName entityName, string value)
    {
      if (!_entitySpecifications.ContainsKey(entityName))
      {
        _entitySpecifications[entityName] = new EntitySpecification(entityName);
      }
      _entitySpecifications[entityName].AddPattern(value);
    }

    public RecognitionResult Recognize(string text)
    {
      text = Normalize(text);
      var tokensUnderPreparation = Tokenize(text);
      var recognizedEntities = tokensUnderPreparation.TranslateToEntitiesUsing(_entitySpecifications.Values);

      return new RecognitionResult(recognizedEntities.ToImmutableList(), TopIntent(recognizedEntities));
    }

    private string TopIntent(IEnumerable<RecognizedEntity> recognizedEntities)
    {
      if (_intentSpecification != null) //todo remove later
      {
        if (_intentSpecification.IsMatchedBy(recognizedEntities))
        {
          return _intentSpecification.IntentName;
        }
        return IntentNone;
      }

      return IntentNone;
    }

    private string Normalize(string text)
    {
      return new Regex(@"\s+").Replace(text, " ");
    }

    private TokensUnderPreparation Tokenize(string text)
    {
      var tokensUnderPreparation = TokensUnderPreparation.CreateInitial(text);

      foreach (var entitySpecification in _entitySpecifications.Values)
      {
        entitySpecification.ApplyTo(tokensUnderPreparation);
      }

      return tokensUnderPreparation;
    }

    public void AddIntent(string intentName, IEnumerable<EntityName> entityNames)
    {
      _intentSpecification = new IntentSpecification(intentName, entityNames);
    }
  }
}