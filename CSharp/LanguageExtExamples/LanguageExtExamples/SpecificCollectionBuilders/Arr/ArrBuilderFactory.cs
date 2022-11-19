using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Arr;

public class ArrBuilderFactory<TElement> : ICollectionBuilderFactory<Arr<TElement>, TElement>
{
  public ICollectionBuilder<Arr<TElement>, TElement> NewBuilder()
  {
    return new ArrBuilder<TElement>();
  }
}