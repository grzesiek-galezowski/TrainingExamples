using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Arr;

public class ArrBuilder<TElement> : ICollectionBuilder<Arr<TElement>, TElement>
{
  private Arr<TElement> _current;

  public Arr<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Add(element);
  }
}