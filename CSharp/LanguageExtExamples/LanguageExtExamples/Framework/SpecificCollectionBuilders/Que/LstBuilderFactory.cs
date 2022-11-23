using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Que;

public class QueBuilderFactory<TElement> : ICollectionBuilderFactory<Que<TElement>, TElement>
{
  public ICollectionBuilder<Que<TElement>, TElement> NewBuilder()
  {
    return new QueBuilder<TElement>();
  }
}