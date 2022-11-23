using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Set;

public class SetBuilderFactory<TElement> : ICollectionBuilderFactory<LanguageExt.Set<TElement>, TElement>
{
  public ICollectionBuilder<LanguageExt.Set<TElement>, TElement> NewBuilder()
  {
    return new SetBuilder<TElement>();
  }
}