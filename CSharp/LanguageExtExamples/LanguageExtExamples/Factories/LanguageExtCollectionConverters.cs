using LanguageExt;
using LanguageExtExamples.Framework;
using LanguageExtExamples.SpecificCollectionBuilders;

namespace LanguageExtExamples.Factories;

public static class LanguageExtCollectionConverters
{
  public static LanguageExtCollectionConverter ForArr()
  {
    return new LanguageExtCollectionConverter(
      typeof(ArrBuilderFactory<>),
      typeof(Arr<>));
  }

  public static LanguageExtCollectionConverter ForSeq()
  {
    return new LanguageExtCollectionConverter(
      typeof(SeqBuilderFactory<>),
      typeof(Seq<>));
  }

  public static LanguageExtCollectionConverter ForLst()
  {
    return new LanguageExtCollectionConverter(
      typeof(LstBuilderFactory<>),
      typeof(Lst<>));
  }
}