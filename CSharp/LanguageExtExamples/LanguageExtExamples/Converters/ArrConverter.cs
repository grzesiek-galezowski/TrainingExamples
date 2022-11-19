using LanguageExt;
using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.Arr;

namespace LanguageExtCollectionsJson.Converters;

public class ArrConverter : LanguageExtCollectionConverter
{
  public ArrConverter() : base(
    typeof(ArrBuilderFactory<>),
    typeof(Arr<>))
  {
  }
}