using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace TddXt.SimpleNlp
{
  public class RecognitionModel
  {
    private const string IntentNone = "None";
    private readonly List<EntitySpecification> _entitySpecifications = new List<EntitySpecification>();
    private readonly List<IntentSpecification> _intentSpecifications = new List<IntentSpecification>();

    public void AddEntity(EntityName entityName, string value)
    {
      _entitySpecifications.Add(new EntitySpecification(entityName, value, new string[] {}));
    }

    public void AddEntity(EntityName entityName, string value, string[] synonyms)
    {
      _entitySpecifications.Add(new EntitySpecification(entityName, value, synonyms));
    }

    public void AddIntent(string intentName, IEnumerable<EntityName> entityNames)
    {
      _intentSpecifications.Add(new IntentSpecification(intentName, entityNames));
    }

    public RecognitionResult Recognize(string text)
    {
      text = Normalize(text);
      var tokensUnderPreparation = Tokenize(text);
      var recognizedEntities = tokensUnderPreparation.TranslateToEntitiesUsing(_entitySpecifications);

      return new RecognitionResult(recognizedEntities.ToImmutableList(), TopIntent(recognizedEntities));
    }

    private string TopIntent(IEnumerable<RecognizedEntity> recognizedEntities)
    {
      foreach (var intentSpec in _intentSpecifications)
      {
        if (intentSpec.IsMatchedBy(recognizedEntities))
        {
          return intentSpec.IntentName;
        }
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

      foreach (var entitySpecification in _entitySpecifications)
      {
        entitySpecification.ApplyTo(tokensUnderPreparation);
      }

      return tokensUnderPreparation;
    }

  }
}