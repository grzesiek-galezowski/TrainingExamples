using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.HashSet;

namespace LanguageExtCollectionsJson.Converters;

public class HashSetConverter : LanguageExtCollectionConverter
{
  public HashSetConverter() : base(
    typeof(HashSetBuilderFactory<>),
    typeof(LanguageExt.HashSet<>))
  {
  }
}