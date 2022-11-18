using LanguageExt;
using LanguageExtExamples.Framework;

namespace LanguageExtExamples.SpecificCollectionBuilders;

public class ArrBuilderFactory<TElement> : ICollectionBuilderFactory<Arr<TElement>, TElement>
{
  public ICollectionBuilder<Arr<TElement>, TElement> NewBuilder()
  {
    return new ArrBuilder<TElement>();
  }
}