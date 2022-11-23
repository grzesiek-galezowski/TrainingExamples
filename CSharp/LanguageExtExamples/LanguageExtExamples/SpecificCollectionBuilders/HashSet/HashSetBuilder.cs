using LanguageExtCollectionsJson.Framework;

namespace LanguageExtCollectionsJson.SpecificCollectionBuilders.HashSet;

public class HashSetBuilder<TElement> : ICollectionBuilder<LanguageExt.HashSet<TElement>, TElement>
{
  private LanguageExt.HashSet<TElement> _current;

  public LanguageExt.HashSet<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Add(element);
  }
}