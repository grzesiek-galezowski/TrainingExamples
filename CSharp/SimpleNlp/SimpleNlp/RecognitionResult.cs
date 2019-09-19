using System.Collections.Immutable;

namespace TddXt.SimpleNlp
{
  public class RecognitionResult
  {
    public RecognitionResult(IImmutableList<RecognizedEntity> entities, string topIntent)
    {
      Entities = entities;
      TopIntent = topIntent;
    }

    public IImmutableList<RecognizedEntity> Entities { get; }
    public string TopIntent { get; }
  }
}