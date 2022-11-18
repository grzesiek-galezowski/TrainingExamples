using LanguageExt;
using LanguageExtExamples.Framework;

namespace LanguageExtExamples.SpecificCollectionBuilders;

public class LstBuilderFactory<TElement> : ICollectionBuilderFactory<Lst<TElement>, TElement>
{
  public ICollectionBuilder<Lst<TElement>, TElement> NewBuilder()
  {
    return new LstBuilder<TElement>();
  }
}