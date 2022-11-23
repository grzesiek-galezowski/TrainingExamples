using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.HashSet;

public class HashSetBuilderFactory<TElement> : ICollectionBuilderFactory<LanguageExt.HashSet<TElement>, TElement>
{
  public ICollectionBuilder<LanguageExt.HashSet<TElement>, TElement> NewBuilder()
  {
    return new HashSetBuilder<TElement>();
  }
}