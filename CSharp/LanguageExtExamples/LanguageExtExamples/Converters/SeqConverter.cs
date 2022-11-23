using LanguageExt;
using LanguageExtCollectionsJson.Framework;
using LanguageExtCollectionsJson.SpecificCollectionBuilders.Seq;

namespace LanguageExtCollectionsJson.Converters;

public class SeqConverter : LanguageExtCollectionConverter
{
  public SeqConverter() : base(
    typeof(SeqBuilderFactory<>),
    typeof(Seq<>))
  {
  }
}