using LanguageExt;
using LanguageExtExamples.Framework;

namespace LanguageExtExamples.SpecificCollectionBuilders;

public class SeqBuilderFactory<TElement> : ICollectionBuilderFactory<Seq<TElement>, TElement>
{
  public ICollectionBuilder<Seq<TElement>, TElement> NewBuilder()
  {
    return new SeqBuilder<TElement>();
  }
}