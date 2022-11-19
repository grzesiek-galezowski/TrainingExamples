using LanguageExt;
using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.Lst;

namespace LanguageExtCollectionsJson.Converters;

public class LstConverter : LanguageExtCollectionConverter
{
  public LstConverter() : base(
    typeof(LstBuilderFactory<>),
    typeof(Lst<>))
  {
  }
}