using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.Set;

public class SetBuilder<TElement> : ICollectionBuilder<LanguageExt.Set<TElement>, TElement>
{
  private LanguageExt.Set<TElement> _current;

  public LanguageExt.Set<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Add(element);
  }
}