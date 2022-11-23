using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Lst;

public class LstBuilderFactory<TElement> : ICollectionBuilderFactory<Lst<TElement>, TElement>
{
  public ICollectionBuilder<Lst<TElement>, TElement> NewBuilder()
  {
    return new LstBuilder<TElement>();
  }
}