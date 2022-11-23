using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Seq;

public class SeqBuilderFactory<TElement> : ICollectionBuilderFactory<Seq<TElement>, TElement>
{
  public ICollectionBuilder<Seq<TElement>, TElement> NewBuilder()
  {
    return new SeqBuilder<TElement>();
  }
}