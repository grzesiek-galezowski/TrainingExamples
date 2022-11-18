using LanguageExt;
using LanguageExtExamples.Framework;

namespace LanguageExtExamples.SpecificCollectionBuilders;

public class SeqBuilder<TElement> : ICollectionBuilder<Seq<TElement>, TElement>
{
  private Seq<TElement> _current;

  public Seq<TElement> Build()
  {
    return _current;
  }

  public void Add(TElement element)
  {
    _current = _current.Add(element);
  }
}