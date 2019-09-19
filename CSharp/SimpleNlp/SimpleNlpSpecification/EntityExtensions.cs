using System.Linq;
using FluentAssertions;
using TddXt.SimpleNlp;

namespace SimpleNlpSpecification
{
  public static class EntityExtensions
  {
    public static void ShouldContainOnly(this RecognitionResult result1, string entityName, string entityValue)
    {
      result1.Entities.Should().HaveCount(1);
      result1.Entities.Single().Should().Be(
        RecognizedEntity.Value(EntityName.Value(entityName), entityValue));
    }
  }
}