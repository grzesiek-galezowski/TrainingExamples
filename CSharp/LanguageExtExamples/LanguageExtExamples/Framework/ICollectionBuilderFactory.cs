namespace LanguageExtExamples.Framework;

public interface ICollectionBuilderFactory<out TCollection, in TElement> where TCollection : IEnumerable<TElement>
{
  ICollectionBuilder<TCollection, TElement> NewBuilder();
}