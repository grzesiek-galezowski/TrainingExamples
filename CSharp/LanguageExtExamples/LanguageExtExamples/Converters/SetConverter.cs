using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.Set;

namespace LanguageExtCollectionsJson.Converters;

public class SetConverter : LanguageExtCollectionConverter
{
  public SetConverter() : base(
    typeof(SetBuilderFactory<>),
    typeof(LanguageExt.Set<>))
  {
  }
}