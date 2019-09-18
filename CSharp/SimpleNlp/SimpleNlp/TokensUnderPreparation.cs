using System.Collections.Generic;

namespace SimpleNlp
{
  public class TokensUnderPreparation
  {
    private string[] _tokens;

    public TokensUnderPreparation(string[] tokens)
    {
      _tokens = tokens;
    }

    public void PartitionBasedOn(string synonym)
    {
      var newTokens = new List<string>();
      foreach (var token in _tokens)
      {
        var strings = token.SplitAndKeep(synonym);
        newTokens.AddRange(strings);
      }

      _tokens = newTokens.ToArray();
    }

    public static TokensUnderPreparation CreateInitial(string text)
    {
      return new TokensUnderPreparation(new[] {text});
    }

    public IEnumerable<RecognizedEntity> TranslateToEntitiesUsing(IEnumerable<EntitySpecification> entitySpecifications)
    {
      var recognizedEntities = new List<RecognizedEntity>();
      foreach (var token in _tokens)
      {
        foreach (var entitySpecification in entitySpecifications)
        {
          entitySpecification.TryToMatch(token, recognizedEntities);
        }
      }

      return recognizedEntities;
    }
  }
}