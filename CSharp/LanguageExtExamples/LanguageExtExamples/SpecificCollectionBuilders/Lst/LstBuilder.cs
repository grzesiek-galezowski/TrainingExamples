using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Lst;

public class LstBuilder<TElement> : ICollectionBuilder<Lst<TElement>, TElement>
{
  private Lst<TElement> _current;

  public Lst<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Add(element);
  }
}