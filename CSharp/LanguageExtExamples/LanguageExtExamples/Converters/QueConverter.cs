using LanguageExt;
using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.Que;

namespace LanguageExtCollectionsJson.Converters;

public class QueConverter : LanguageExtCollectionConverter
{
  public QueConverter() : base(
    typeof(QueBuilderFactory<>),
    typeof(Que<>))
  {
  }
}