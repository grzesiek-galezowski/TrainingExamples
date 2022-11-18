namespace LanguageExtExamples.Framework;

public interface ICollectionBuilder<out TCollection, in TElement> where TCollection : IEnumerable<TElement>
{
  TCollection Build();
  void Add(TElement element);
}