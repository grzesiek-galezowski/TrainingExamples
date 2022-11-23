using LanguageExt;
using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Que;

public class QueBuilder<TElement> : ICollectionBuilder<Que<TElement>, TElement>
{
  private Que<TElement> _current;

  public Que<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Enqueue(element);
  }
}